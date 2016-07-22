// FilExileExt.cpp : Implementation of DLL Exports.

#include "stdafx.h"
#include "resource.h"
#include "FilExileExt_i.h"
#include "dllmain.h"
#include "atlstr.h"
#include <string>

using namespace ATL;
using namespace std;

// Used to determine whether the DLL can be unloaded by OLE.
STDAPI DllCanUnloadNow(void)
{
			return _AtlModule.DllCanUnloadNow();
	}

// Returns a class factory to create an object of the requested type.
_Check_return_
STDAPI DllGetClassObject(_In_ REFCLSID rclsid, _In_ REFIID riid, _Outptr_ LPVOID* ppv)
{
		return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}

// DllRegisterServer - Adds entries to the system registry.
STDAPI DllRegisterServer(void)
{
	// registers object, typelib and all interfaces in typelib
	HRESULT hr = _AtlModule.DllRegisterServer();
		return hr;
}

// DllUnregisterServer - Removes entries from the system registry.
STDAPI DllUnregisterServer(void)
{
	HRESULT hr = _AtlModule.DllUnregisterServer();
		return hr;
}

// DllInstall - Adds/Removes entries to the system registry per user per machine.
STDAPI DllInstall(BOOL bInstall, _In_opt_  LPCWSTR pszCmdLine)
{
	HRESULT hr = E_FAIL;
	static const wchar_t szUserSwitch[] = L"user";

	if (pszCmdLine != NULL)
	{
		if (_wcsnicmp(pszCmdLine, szUserSwitch, _countof(szUserSwitch)) == 0)
		{
			ATL::AtlSetPerUserRegistration(true);
		}
	}

	if (bInstall)
	{	
		hr = DllRegisterServer();
		if (FAILED(hr))
		{
			DllUnregisterServer();
		}
	}
	else
	{
		hr = DllUnregisterServer();
	}

	return hr;
}

string wstrToStr(const wstring &wstr)
{
	string strTo;
	char *szTo = new char[wstr.length() + 1];
	szTo[wstr.size()] = '\0';
	WideCharToMultiByte(CP_ACP, 0, wstr.c_str(), -1, szTo, (int)wstr.length(), NULL, NULL);
	strTo = szTo;
	delete[] szTo;
	return strTo;
}

wstring strToWstr(const string &str)
{
	wstring wstrTo;
	wchar_t *wszTo = new wchar_t[str.length() + 1];
	wszTo[str.size()] = L'\0';
	MultiByteToWideChar(CP_ACP, 0, str.c_str(), -1, wszTo, (int)str.length());
	wstrTo = wszTo;
	delete[] wszTo;
	return wstrTo;
}

// FilExileExt.cpp : Implementation of CFilExileShlExt

#include "stdafx.h"
#include "FilExileShlExt.h"
#include <atlconv.h>

// CFilExileShlExt

STDMETHODIMP CFilExileShlExt::Initialize(
	LPCITEMIDLIST pidlFolder,
	LPDATAOBJECT pDataObj,
	HKEY hProgID)
{
	FORMATETC fmt = { CF_HDROP, NULL, DVASPECT_CONTENT,
		-1, TYMED_HGLOBAL };
	STGMEDIUM stg = { TYMED_HGLOBAL };
	HDROP hDrop;

	if (FAILED(pDataObj->GetData(&fmt, &stg)))
		return E_INVALIDARG;

	hDrop = (HDROP)GlobalLock(stg.hGlobal);

	if (hDrop == NULL)
		return E_INVALIDARG;

	UINT uNumFiles = DragQueryFile(hDrop, 0xFFFFFFFF, NULL, 0);
	HRESULT hr = S_OK;

	if (uNumFiles == 0)
	{
		GlobalUnlock(stg.hGlobal);
		ReleaseStgMedium(&stg);
		return E_INVALIDARG;
	}

	if (DragQueryFile(hDrop, 0, m_szFile, MAX_PATH) == 0)
		hr = E_INVALIDARG;

	GlobalUnlock(stg.hGlobal);
	ReleaseStgMedium(&stg);

	return hr;
}

// This method controls how the shell extension appears in the context menu
HRESULT CFilExileShlExt::QueryContextMenu(
	HMENU hMenu, UINT uMenuIndex, UINT uidFirstCmd,
	UINT uidLastCmd, UINT uFlags)
{
	ATL::CString contextLbl;
	contextLbl.LoadString(IDS_CONTEXTLBL);

	if (uFlags & CMF_DEFAULTONLY)
		return MAKE_HRESULT(SEVERITY_SUCCESS, FACILITY_NULL, 0);

	InsertMenu(hMenu, uMenuIndex, MF_BYPOSITION,
		uidFirstCmd, contextLbl);

	HBITMAP hBitmap = (HBITMAP)LoadImage(GetModuleHandle(L"FilExileExt"), MAKEINTRESOURCE(BITMAP_MAIN), 
		IMAGE_BITMAP, 0, 0, LR_DEFAULTSIZE | LR_LOADTRANSPARENT | LR_LOADMAP3DCOLORS);
	SetMenuItemBitmaps(hMenu, uMenuIndex, MF_BYPOSITION, hBitmap, hBitmap);

	return MAKE_HRESULT(SEVERITY_SUCCESS, FACILITY_NULL, 1);
}

// This method handles the fly-by help if requested by Windows
HRESULT CFilExileShlExt::GetCommandString(
	UINT_PTR idCmd, UINT uFlags, UINT* pwReserved,
	LPSTR pszName, UINT cchMax)
{
	USES_CONVERSION;

	if (idCmd != 0)
		return E_INVALIDARG;

	if (uFlags & GCS_HELPTEXT)
	{
		ATL::CString strMyString;
		strMyString.LoadString(IDS_HELPSTRING);

		if (uFlags & GCS_UNICODE)
		{
			lstrcpynW((LPWSTR)pszName, T2CW(strMyString), cchMax);
		}
		else
		{
			lstrcpynA(pszName, T2CA(strMyString), cchMax);
		}

		return S_OK;
	}

	return E_INVALIDARG;
}

// This method handles the actual execution of the context menu action
HRESULT CFilExileShlExt::InvokeCommand(
	LPCMINVOKECOMMANDINFO pCmdInfo)
{
	if (HIWORD(pCmdInfo->lpVerb) != 0)
		return E_INVALIDARG;

	switch (LOWORD(pCmdInfo->lpVerb))
	{
	case 0:
	{
		WCHAR cmdPath[MAX_PATH];
		WCHAR szMsg[MAX_PATH];

		STARTUPINFO si;
		PROCESS_INFORMATION pi;
		ZeroMemory(&si, sizeof(si));
		si.cb = sizeof(si);
		ZeroMemory(&pi, sizeof(pi));

		// Get the installation path for FilExile
		string exePath; HKEY hKey = 0; WCHAR szBuffer[512];
		DWORD dwType = REG_SZ; DWORD dwBufSize = sizeof(szBuffer);
		if (RegOpenKey(HKEY_LOCAL_MACHINE, L"SOFTWARE\\FilExile", &hKey) == ERROR_SUCCESS)
		{
			if (RegQueryValueEx(hKey, L"Path", NULL, &dwType, (LPBYTE)szBuffer, &dwBufSize) == ERROR_SUCCESS)
			{
				exePath = wstrToStr(szBuffer);
			}

			RegCloseKey(hKey);
		}
		// Check the alternative path that it can also be found at
		else if (RegOpenKey(HKEY_LOCAL_MACHINE, L"SOFTWARE\\WOW6432Node\\FilExile", &hKey) == ERROR_SUCCESS)
		{
			if (RegQueryValueEx(hKey, L"Path", NULL, &dwType, (LPBYTE)szBuffer, &dwBufSize) == ERROR_SUCCESS)
			{
				exePath = wstrToStr(szBuffer);
			}

			RegCloseKey(hKey);
		}

		// Figure out if we're supposed to show a dialog after deleting the file
		int showDialog = 1; unsigned long type = REG_DWORD, size = 1024; DWORD dwBuffer;
		if (RegOpenKey(HKEY_LOCAL_MACHINE, L"SOFTWARE\\FilExile", &hKey) == ERROR_SUCCESS)
		{
			if (RegQueryValueEx(hKey, L"ShowDialog", NULL, &type, (LPBYTE)&dwBuffer, &size) == ERROR_SUCCESS)
			{
				showDialog = dwBuffer;
			}

			RegCloseKey(hKey);
		}
		// Check the alternative path that it can also be found at
		else if (RegOpenKey(HKEY_LOCAL_MACHINE, L"SOFTWARE\\WOW6432Node\\FilExile", &hKey) == ERROR_SUCCESS)
		{
			if (RegQueryValueEx(hKey, L"ShowDialog", NULL, &type, (LPBYTE)&dwBuffer, &size) == ERROR_SUCCESS)
			{
				showDialog = dwBuffer;
			}

			RegCloseKey(hKey);
		}

		// Need to concatenate the path (value) and file (m_szFile)
		// e.g.: "C:\\filexile\\FilExile\\bin\\Debug\\FilExile.exe C:\\file_to_delete.txt"
		string filePath = wstrToStr(m_szFile);
		string combined = "\"" + exePath + "\" \"" + filePath + "\"";
		wcscpy_s(cmdPath, strToWstr(combined).c_str());

		if 
		(
			!CreateProcess(NULL,
				cmdPath,	// Command line
				NULL,		// Process handle not inheritable
				NULL,		// Thread handle not inheritable
				FALSE,		// Set handle inheritance to FALSE
				0,			// No creation flags
				NULL,		// Use parent's environment block
				NULL,		// Use parent's starting directory
				&si,		// Pointer to STARTUPINFO structure
				&pi)		// Pointer to PROCESS_INFORMATION structure
		)
		{
			return S_FALSE;
		}
		
		WaitForSingleObject(pi.hProcess, INFINITE);

		CloseHandle(pi.hProcess);
		CloseHandle(pi.hThread);

		// If permitted by the registry key "ShowDialog" - display a message saying the file was deleted
		if (showDialog != 0)
		{
			ATL::CString deletedString;
			deletedString.LoadString(IDS_DELETED);
			wcscpy_s(szMsg, m_szFile + deletedString);
			MessageBox(pCmdInfo->hwnd, szMsg, _T("FilExile"),
				MB_ICONINFORMATION);
		}

		return S_OK;
	}
	break;

	default:
		return E_INVALIDARG;
		break;
	}
}
