using Almacen._keenthemes.libs;

namespace Almacen._keenthemes;

public interface IBootstrapBase
{
    void InitThemeMode();
    
    void InitThemeDirection();
    
    void InitRtl();

    void InitLayout();

    void Init(IKTTheme theme);
}