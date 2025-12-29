using System.Data;
using conexion;

namespace negocio
{
    public class NEGOCIO
    {
        private DATOS objData = new DATOS();
        public int AgregarDatos(string Procedure, params object[] Argumentos)
        {
            int AgregarDatosRet = default;
            AgregarDatosRet = objData.Ejecutar(Procedure, Argumentos);
            return AgregarDatosRet;
        }
        public DataSet MostraDatos(string Procedure, params object[] Argumentos)
        {
            DataSet MostraDatosRet = default;
            // aqui extrae datos para la consulta 
            MostraDatosRet = objData.TraerDataset(Procedure, Argumentos);
            return MostraDatosRet;
        }
    }
}