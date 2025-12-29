using System.Data;
using System.Data.SqlClient;

namespace conexion
{
    public class DATOS
    {
        private bool InstanceFieldsInitialized = false;

        public DATOS()
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
        }

        private void InitializeInstanceFields()
        {
            Cn = new SqlConnection(Convert.ToString(callSQL()));
        }

        private int desarrollo;
        public string SiteKeyM { get; set; }
        public string SecretKeyM { get; set; }

        public object callSQL()
        {

            const string menchorSiteKey = "6LeqzH0lAAAAACMIcK34AJZ16hjXnWvjdFAvIA2K";
            const string menchorSecretKey = "6LeqzH0lAAAAAEaNTCJ4WVMDa1tPR3j1y_WxcJnJ";

            const string demoSiteKey = "6LdJi6QlAAAAAAQk4ht6BTI7R28At-7sg9lpXjY1";
            const string demoSecretKey = "6LdJi6QlAAAAANjfbFBlKUma-Qu4bJEy-TUrnv9D";


            SiteKeyM = demoSiteKey;
            SecretKeyM = demoSecretKey;

            desarrollo = 3;

            //claves de seguridad de sitios







            string ServerNombre = null;
            string Catalogo = null;
            string UserID = null;
            string Passwd = null;

            if (desarrollo == 1)
            {
                ServerNombre = "SERVERMAC";
                Catalogo = "db_a6440e_padronsql";
                UserID = "PadronSQL";
                Passwd = "PadronSQL2024";

            }
            else if (desarrollo == 2)
            {

                ServerNombre = "SQL5104.site4now.net";
                Catalogo = "db_a6440e_padronsql";
                UserID = "db_a6440e_padronsql_admin";
                Passwd = "PadronSQL2024";

            }
            else if (desarrollo == 3)
            {

                ServerNombre = "PC963446";
                Catalogo = "informes";
                UserID = "SoporteVeraguas";
                Passwd = "sqlveraguas";
            }

            return "Data Source=" + ServerNombre + "; Initial Catalog=" + Catalogo + ";user id=" + UserID + ";password=" + Passwd + ";Integrated Security=false";

        }

        public SqlConnection Cn;
        #region Poner Parametros
        private static System.Collections.Hashtable mColComandos = new System.Collections.Hashtable();
        protected System.Data.IDbCommand Comando(string ProcedimientoAlmacenado)
        {
            System.Data.SqlClient.SqlCommand mComando = null;
            if (mColComandos.Contains(ProcedimientoAlmacenado))
            {
                mComando = (System.Data.SqlClient.SqlCommand)mColComandos[ProcedimientoAlmacenado];
            }
            else
            {
                Cn.Open();
                mComando = new System.Data.SqlClient.SqlCommand(ProcedimientoAlmacenado, Cn);
                System.Data.SqlClient.SqlCommandBuilder mContructor = new System.Data.SqlClient.SqlCommandBuilder();
                mComando.Connection = Cn;
                mComando.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(mComando);
                Cn.Close();
                mColComandos.Add(ProcedimientoAlmacenado, mComando);
            }
            return mComando;
        }
        protected void CargarParametros(System.Data.IDbCommand Comando, object[] Args)
        {
            int I = 0;
            for (I = 0; I <= Args.GetUpperBound(0); I++)
            {
                try
                {
                    ((System.Data.SqlClient.SqlParameter)Comando.Parameters[I + 1]).Value = Args[I];
                }
                catch (Exception Qex)
                {
                    throw (Qex);
                }
            }
        }
        #endregion
        #region Devolver Parametros
        protected System.Data.IDataAdapter CrearDataAdapter(string ProcedimientoAlmacenado, params object[] Args)
        {
            System.Data.SqlClient.SqlCommand mCom = (SqlCommand)Comando(ProcedimientoAlmacenado);
            if (Args != null)
            {
                CargarParametros(mCom, Args);
            }
            return new System.Data.SqlClient.SqlDataAdapter(mCom);
        }
        //En este caso trabajaremos con funciones sobrecargadas con la finalidad de poder llamar a la ‘misma function pero con diferentes parametros. 
        public System.Data.DataSet TraerDataset(string ProcedimientoAlmacenado)
        {
            System.Data.DataSet mDataset = new System.Data.DataSet();
            CrearDataAdapter(ProcedimientoAlmacenado).Fill(mDataset);
            return mDataset;
        }
        //Funcion Sobrecargada 
        public System.Data.DataSet TraerDataset(string ProcedimientoAlmacenado, params System.Object[] Argumentos)
        {
            System.Data.DataSet mDataset = new System.Data.DataSet();
            CrearDataAdapter(ProcedimientoAlmacenado, Argumentos).Fill(mDataset);
            return mDataset;
        }
        #endregion
        #region Acciones
        public int Ejecutar(string ProcedimientoAlmacenado, params System.Object[] Argumentos)
        {
            System.Data.SqlClient.SqlCommand mCom = (SqlCommand)Comando(ProcedimientoAlmacenado);
            int Resp = 0;
            Cn.Open();
            mCom.Connection = Cn;
            mCom.CommandType = CommandType.StoredProcedure;
            CargarParametros(mCom, Argumentos);
            Resp = mCom.ExecuteNonQuery();
            Cn.Close();
            return Resp;
        }
        #endregion
    }
}