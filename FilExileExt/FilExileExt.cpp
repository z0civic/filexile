// FilExileExt.cpp : Implementation of DLL Exports.

#include "stdafx.h"
#include "resource.h"
#include "FilExileExt_i.h"
#include "dllmain.h"
#include "atlstr.h"
#include <string.h>

using namespace ATL;

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
	//TODO: Set icon
	CString contextLbl;
	contextLbl.LoadString(IDS_CONTEXTLBL);

	if (uFlags & CMF_DEFAULTONLY)
		return MAKE_HRESULT(SEVERITY_SUCCESS, FACILITY_NULL, 0);

	InsertMenu(hMenu, uMenuIndex, MF_BYPOSITION,
		uidFirstCmd, contextLbl);

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
		CString strMyString;
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
		TCHAR szMsg[MAX_PATH + 32];

		STARTUPINFO si;
		PROCESS_INFORMATION pi;
		ZeroMemory(&si, sizeof(si));
		si.cb = sizeof(si);
		ZeroMemory(&pi, sizeof(pi));

		// Get the installation path for FilExile
		WCHAR* value; HKEY hKey = 0; WCHAR szBuffer[512];
		DWORD dwType = REG_SZ; DWORD dwBufSize = sizeof(szBuffer);
		if (RegOpenKey(HKEY_LOCAL_MACHINE, L"SOFTWARE\\FilExile", &hKey) == ERROR_SUCCESS)
		{
			if (RegQueryValueEx(hKey, L"Path", NULL, &dwType, (LPBYTE) szBuffer, &dwBufSize) == ERROR_SUCCESS)
			{
				value = szBuffer;
			}
			RegCloseKey(hKey);
		}

		WCHAR cmdPath[255];

		wsprintf(szMsg, L"", m_szFile);

		if 
		(
			!CreateProcess(NULL,
				szMsg,		// Command line
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

		return S_OK;
	}
	break;

	default:
		return E_INVALIDARG;
		break;
	}
}
