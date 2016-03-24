// dllmain.h : Declaration of module class.

class CFilExileExtModule : public ATL::CAtlDllModuleT< CFilExileExtModule >
{
public :
	DECLARE_LIBID(LIBID_FilExileExtLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_FILEXILESHLEXT, "{37D0B08A-2D0E-4A2E-8C8D-B2CB52BA81AC}")
};

extern class CFilExileExtModule _AtlModule;
