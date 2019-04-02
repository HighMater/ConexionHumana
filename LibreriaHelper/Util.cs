using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibreriaHelper
{
    public static class Util
    {
        /// <summary>
        /// Muestra un mensaje en la pagina
        /// </summary>
        /// <param name="pageObj"></param>
        /// <param name="messageBody"></param>
        public static void showMessage(Page pageObj, string messageBody)
        {
            messageBody = messageBody.Replace('\'', '"');
            string token = Guid.NewGuid().ToString("N") + DateTime.Now.ToString("ssfff");
            string alertScript = "$.confirm({"
                + "title: 'Alerta',"
                + "useBootstrap: false,"
                + "boxWidth: '30%',"
                + "content: '" + messageBody + "',"
                + "type: 'blue',"
                + "typeAnimated: true,"
                + "buttons:{"
                        + "OK: function() {}"
                    + "}"
                + "});";

            pageObj.ClientScript.RegisterStartupScript(
                pageObj.GetType()
                , "script_" + token
                , alertScript
                , true
            );
        }

        /// <summary>
        /// Muestra un mensaje en la pagina
        /// </summary>
        /// <param name="pageObj"></param>
        /// <param name="messageBody"></param>
        /// /// <param name="messageType"></param>
        public static void showMessage(Page pageObj, string messageBody, string messageType)
        {
            messageBody = messageBody.Replace('\'', '"');
            string token = Guid.NewGuid().ToString("N") + DateTime.Now.ToString("ssfff");
            string messageColor = messageType.ToLower() == "error" || messageType.ToLower() == "err" ? "red" : messageType.ToLower() == "warning" || messageType.ToLower() == "warn" ? "orange" : "blue";
            string alertScript = "$.confirm({"
                + "title: 'Alerta',"
                + "content: '" + messageBody + "',"
                + "type: '" + messageColor + "',"
                + "useBootstrap: false,"
                + "typeAnimated: true,"
                + "boxWidth: '30%',"
                + "buttons:{"
                        + "OK: function() {}"
                    + "}"
                + "});";

            pageObj.ClientScript.RegisterStartupScript(
                pageObj.GetType()
                , "script_" + token
                , alertScript
                , true
            );
        }

        /// <summary>
        /// Ejecuta una funcion de javascript
        /// </summary>
        /// <param name="pageObj"></param>
        /// <param name="jsFunction"></param>
        public static void execJS(Page pageObj, string jsFunction)
        {
            string token = Guid.NewGuid().ToString("N") + DateTime.Now.ToString("ssfff");
            jsFunction = jsFunction.Substring(jsFunction.Length - 1) == ";" ? jsFunction : jsFunction + ";";

            pageObj.ClientScript.RegisterStartupScript(
                pageObj.GetType()
                , "script_" + token
                , jsFunction
                , true
            );
        }

        /// <summary>
        /// Obtiene el valor de un control
        /// </summary>
        /// <param name="controlType"></param>
        /// <param name="controlTarget"></param>
        /// <param name="idControl"></param>
        /// <returns></returns>
        public static string getElementValue(string controlType, Control controlTarget, string idControl)
        {
            if (controlTarget != null)
            {
                try
                {
                    if (controlType.ToLower() == "textbox")
                    {
                        TextBox txt = (TextBox)controlTarget.FindControl(idControl);
                        if (txt != null)
                        {
                            return txt.Text;
                        }
                    }
                    else if (controlType.ToLower() == "hidden")
                    {
                        HiddenField hdn = (HiddenField)controlTarget.FindControl(idControl);
                        if (hdn != null)
                        {
                            return hdn.Value;
                        }
                    }
                    else if (controlType.ToLower() == "dropdownlist")
                    {
                        DropDownList ddl = (DropDownList)controlTarget.FindControl(idControl);
                        if (ddl != null)
                        {
                            return ddl.SelectedValue;
                        }
                    }
                    else if (controlType.ToLower() == "checkbox")
                    {
                        CheckBox chk = (CheckBox)controlTarget.FindControl(idControl);
                        if (chk != null)
                        {
                            return chk.Checked.ToString();
                        }
                    }
                }
                catch (Exception err)
                {
                    Console.Write(err.Message);
                    return "";
                }
            }
            // Si no encuentra nada, regresa empty string
            return "";
        }

        /// <summary>
        /// Obtiene el valor de un elemento anidado SOLO un nivel abajo
        /// Soporta: Textbox, HiddenField, DropDownList y CheckBox
        /// </summary>
        /// <param name="controlTarget"></param>
        /// <param name="idControl"></param>
        /// <returns></returns>
        public static string getElementValue(Control controlTarget, string idControl) {
            if (controlTarget != null) {
                object myCtrl = controlTarget.FindControl(idControl);

                if (myCtrl != null) {
                    string controlType = myCtrl.GetType().Name;

                    try {
                        if (controlType.ToLower() == "textbox") {
                            TextBox txt = (TextBox)myCtrl;
                            return txt.Text;
                        } else if (controlType.ToLower() == "hidden") {
                            HiddenField hdn = (HiddenField)controlTarget.FindControl(idControl);
                            return hdn.Value;
                        } else if (controlType.ToLower() == "dropdownlist") {
                            DropDownList ddl = (DropDownList)controlTarget.FindControl(idControl);
                            return ddl.SelectedValue;
                        } else if (controlType.ToLower() == "checkbox") {
                            CheckBox chk = (CheckBox)controlTarget.FindControl(idControl);
                            return chk.Checked.ToString();
                        } else if (controlType.ToLower() == "radiobutton") {
                            RadioButton rdb = (RadioButton)controlTarget.FindControl(idControl);
                            return rdb.Checked.ToString();
                        }
                    } catch (Exception err) {
                        Console.Write(err.Message);
                        return "";
                    }
                } else {
                    return "";
                }
            }
            // Si no encuentra nada, regresa empty string
            return "";
        }

        /// <summary>
        /// Establece el valor a un control anidado SOLO un nivel abajo
        /// Soporta: Textbox, HiddenField, DropDownList y CheckBox
        /// </summary>
        /// <param name="controlType"></param>
        /// <param name="controlTarget"></param>
        /// <param name="idControl"></param>
        /// <param name="val"></param>
        public static void setElementValue(Control controlTarget, string idControl, string val)
        {
            if (controlTarget != null) {
                object myCtrl = controlTarget.FindControl(idControl);

                if (myCtrl != null) {
                    string controlType = myCtrl.GetType().Name;

                    try {
                        if (controlType.ToLower() == "textbox") {
                            TextBox txt = (TextBox)myCtrl;
                            txt.Text = val;
                        } else if (controlType.ToLower() == "hidden") {
                            HiddenField hdn = (HiddenField)controlTarget.FindControl(idControl);
                            hdn.Value = val;
                        } else if (controlType.ToLower() == "dropdownlist") {
                            DropDownList ddl = (DropDownList)controlTarget.FindControl(idControl);
                            ddl.SelectedValue = val;
                        } else if (controlType.ToLower() == "checkbox") {
                            CheckBox chk = (CheckBox)controlTarget.FindControl(idControl);
                            chk.Checked = Lib.getBool(val);
                        } else if (controlType.ToLower() == "radiobutton") {
                            RadioButton rdb = (RadioButton)controlTarget.FindControl(idControl);
                            rdb.Checked = Lib.getBool(val);
                        }
                    } catch (Exception err) {
                        Console.Write(err.Message);
                        return;
                    }
                } else {
                    return;
                }
            }
        }

        public static void setElementValue(string controlType, Control controlTarget, string idControl, string val)
        {
            try
            {
                if (controlType.ToLower() == "textbox")
                {
                    TextBox txt = (TextBox)controlTarget.FindControl(idControl);
                    if (txt != null)
                    {
                        txt.Text = val;
                    }
                }
                else if (controlType.ToLower() == "hidden")
                {
                    HiddenField hdn = (HiddenField)controlTarget.FindControl(idControl);
                    if (hdn != null)
                    {
                        hdn.Value = val;
                    }
                }
                else if (controlType.ToLower() == "dropdownlist")
                {
                    DropDownList ddl = (DropDownList)controlTarget.FindControl(idControl);
                    if (ddl != null)
                    {
                        ddl.SelectedValue = val;
                    }
                }
                else if (controlType.ToLower() == "checkbox")
                {
                    CheckBox chk = (CheckBox)controlTarget.FindControl(idControl);
                    if (chk != null)
                    {
                        chk.Checked = Lib.getBool(val);
                    }
                }
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
            }
        }

        /// <summary>
        /// Establece un datasource a un dropdown
        /// </summary>
        /// <param name="ddl">Target DropDown</param>
        /// <param name="stp">Store Procedure</param>
        /// <param name="value">Item Value</param>
        /// <param name="text">Item Text</param>
        public static void feedDropdown(DropDownList ddl, string stp, string value, string text)
        {
            if (ddl != null) {
                DataTable dt = FillDataTable(stp, CommandType.StoredProcedure);
                // Llena el datasource del dropdown
                ddl.DataSource = dt;
                ddl.DataTextField = text;
                ddl.DataValueField = value;
                ddl.DataBind();
            } else {
                throw new ArgumentException("El dropdown que esta intentando llenar no existe", "feedDropdown");
            }
        }

        /// <summary>
        /// Establece un datasource a un dropdown
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="stp"></param>
        /// <param name="value"></param>
        /// <param name="text"></param>
        /// <param name="firstValue">Agregar un valor al inicio del dropdown</param>
        public static void feedDropdown(DropDownList ddl, string stp, string value, string text, string firstValue)
        {
            // Llena el datatable
            String cnnStr = ConfigurationManager.ConnectionStrings["BaseDatosPpal"].ConnectionString;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(stp, cnnStr);
            da.Fill(dt);
            // Llena el datasource del dropdown
            ddl.DataSource = dt;
            ddl.DataTextField = text;
            ddl.DataValueField = value;
            ddl.SelectedValue = null;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem(firstValue, "0"));
        }

        /// <summary>
        /// Establece un datasource a un dropdown con parametros
        /// </summary>
        /// <param name="ddl">Target DropDown</param>
        /// <param name="stp">Store Procedure</param>
        /// <param name="value">Item Value</param>
        /// <param name="text">Item Text</param>
        public static void feedDropdown(DropDownList ddl, string stp, string value, string text, string firstValue, SqlParameter[] sqlParams)
        {
            if (ddl != null) {
                DataTable dt = FillDataTable(stp, CommandType.StoredProcedure, sqlParams);
                // Llena el datasource del dropdown
                ddl.DataSource = dt;
                ddl.DataTextField = text;
                ddl.DataValueField = value;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem(firstValue, "0"));
            } else {
                throw new ArgumentException("El dropdown que esta intentando llenar no existe", "feedDropdown");
            }
        }

        public static void feedDropdown(DropDownList ddl, CommandType cmdType, string stp, string value, string text)
        {
            if (ddl != null) {
                // Llena el datatable
                String cnnStr = ConfigurationManager.ConnectionStrings["BaseDatosPpal"].ConnectionString;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(stp, cnnStr);
                da.Fill(dt);
                // Llena el datasource del dropdown
                ddl.DataSource = dt;
                ddl.DataTextField = text;
                ddl.DataValueField = value;
                ddl.DataBind();
            } else {
                throw new ArgumentException("El dropdown que esta intentando llenar no existe", "feedDropdown");
            }
            
        }

        public static void feedDropdown(DropDownList ddl, CommandType cmdType, string stp, string value, string text, string firstValue)
        {
            // Llena el datatable
            String cnnStr = ConfigurationManager.ConnectionStrings["BaseDatosPpal"].ConnectionString;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(stp, cnnStr);
            da.Fill(dt);
            // Llena el datasource del dropdown
            ddl.DataSource = dt;
            ddl.DataTextField = text;
            ddl.DataValueField = value;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem(firstValue, "0"));
        }

        /// <summary>
        /// Establece un valor en el dropdown, si el dropdown no contiene ese valor,
        /// establece el primer valor como default
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="value"></param>
        public static void setDropDownValue(DropDownList ddl, string value) {
            for (int i=0; i < ddl.Items.Count; i++) {
                if (ddl.Items[i].Value == value) {
                    ddl.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Obtiene los datakeys de un Gridview y los regresa separados por coma ","
        /// </summary>
        /// <param name="gv">Gridview Name</param>
        /// <returns>String</returns>
        public static string getDataKeyCollection(GridView gv)
        {
            string data = "";

            for (int keyIndex = 0; keyIndex < gv.SelectedDataKey.Values.Count; keyIndex++)
            {
                data += Lib.getText(gv.SelectedDataKey[keyIndex]) + ",";
            }
            data = data.Trim(',');
            return data;
        }

        /// <summary>
        /// Obtiene los datakeys de un Gridview y los regresa separados por coma ","
        /// </summary>
        /// <param name="gv">Gridview Name</param>
        /// <returns>String</returns>
        public static string getDataKeyCollection(GridView gv, char customSeparator)
        {
            string data = "";

            for (int keyIndex = 0; keyIndex < gv.SelectedDataKey.Values.Count; keyIndex++)
            {
                data += Lib.getText(gv.SelectedDataKey[keyIndex]) + customSeparator;
            }
            data = data.Trim(customSeparator);
            return data;
        }

        /// <summary>
        /// Valida los permisos para el usuario en la asp
        /// </summary>
        /// <param name="P">Page</param>
        /// <param name="idUsuario"></param>
        /// <param name="componente"></param>
        public static void ValidatePermission(Page P, int idUsuario, string componente) {
            string aspName = Path.GetFileName(componente).Trim('/');
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@IdUsuario", idUsuario),
                new SqlParameter("@componente", aspName)
            };

            DataTable dt = FillDataTable("stp_Componente_Permisos_Select_v1", CommandType.StoredProcedure, parm);
            
            var masterPage = P.Master.FindControl("MainContent");

            if (masterPage != null) {
                var btnElimina = masterPage.FindControl("btnEliminar");
                var btnGuardar = masterPage.FindControl("btnGuardar");

                

                if (btnElimina != null) {
                    if (dt.Rows.Count > 0) {
                        DataRow row = dt.Rows[0];
                        btnElimina.Visible = Lib.getInt(row["Elimina"]) > 0;
                        Console.Write(btnElimina.GetType());
                    } else {
                        btnElimina.Visible = false;
                    }
                }

                if (btnGuardar != null) {
                    if (dt.Rows.Count > 0) {
                        DataRow row = dt.Rows[0];
                        btnGuardar.Visible = Lib.getInt(row["Edita"]) > 0;
                    } else {
                        btnGuardar.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Valida permisos, oculta o muestra botones
        /// </summary>
        /// <param name="P">Page</param>
        /// <param name="idUsuario">ID usuario</param>
        /// <param name="componente">Nombre ASPX</param>
        /// <param name="botones">
        ///     Coleccion de botones que se quieren validar separados por ":" Elemento:Validacion.
        ///         Ejemplo: btnEliminar:Elimina, btnGuarda:Edita
        ///     Si un boton esta dentro de algun controlador, se separa por pipe "|"
        ///         Ejemplo: FormViewAbc|btnEliminar:Elimina
        /// </param>
        public static void ValidatePermission(Page P, int idUsuario, string componente, string[] botones) {
            componente = Path.GetFileName(componente);

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@IdUsuario", idUsuario),
                new SqlParameter("@componente", componente)
            };

            DataTable dt = FillDataTable("stp_Componente_Permisos_Select_v1", CommandType.StoredProcedure, parm);

            var masterPage = P.Master.FindControl("MainContent");

            foreach (string boton in botones) {
                string[] data = boton.Split(':');
                string permissionType = data[1].Substring(0,1).ToUpper() + data[1].Substring(1).ToLower();
                var btn = masterPage.FindControl(data[0]);

                // Verifica si el boton esta anidado
                if (data[0].IndexOf("|") >= 0) {
                    string[] elementos = data[0].Split('|');
                    string btnName = elementos[elementos.Length - 1];
                    var element = masterPage.FindControl(elementos[0]);

                    for (int i = 1; i < elementos.Length-1; i++) {
                        element = element.FindControl(elementos[i]);
                    }

                    btn = element;
                }
                
                        
                if (btn != null) {
                    if (dt.Rows.Count > 0) {
                        DataRow row = dt.Rows[0];
                        btn.Visible = Lib.getInt(row[permissionType]) > 0;
                    } else {
                        btn.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el valor del datakey de un gridview al dar click a un boton
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="dataKeyName"></param>
        /// <returns></returns>
        public static object GetDataKeyValue(object ctrl, string dataKeyName) {
            if (ctrl.GetType().Name == "Button") {
                GridViewRow row = (GridViewRow)((Button)ctrl).NamingContainer;
                GridView gv = (GridView)row.NamingContainer;
                return gv.DataKeys[row.RowIndex][dataKeyName];
            } else if (ctrl.GetType().Name == "ImageButton") {
                GridViewRow row = (GridViewRow)((ImageButton)ctrl).NamingContainer;
                GridView gv = (GridView)row.NamingContainer;
                return gv.DataKeys[row.RowIndex][dataKeyName];
            } else {
                return "";
            }
        }

        /// <summary>
        /// (Deprecated)
        /// </summary>
        /// <param name="dt">Data table which feed gridview datasource</param>
        /// <param name="gv">Target gridview</param>
        /// <param name="colName">Name from column you wish to get total</param>
        public static void setTotalColumn(DataTable dt, GridView gv, string colName) {
            if (dt.Rows.Count > 0) {
                int dtColIndex = dt.Columns.IndexOf(colName);
                int gvColIndex = getColumnIndex(gv, colName);

                Type columnType = dt.Columns[dtColIndex].DataType;
                TableCell footer = gv.FooterRow.Cells[gvColIndex];

                if (columnType == typeof(Int32)) {
                    int totalInt = dt.AsEnumerable().Sum(row => row.Field<int?>(dtColIndex) == null ? 0 : row.Field<int>(dtColIndex));
                    footer.Text = totalInt.ToString("N", new CultureInfo("en-us"));
                } else if (columnType == typeof(float) || columnType == typeof(decimal)) {
                    Decimal totalDecimal = dt.AsEnumerable().Sum(row => row.Field<decimal?>(dtColIndex) == null ? 0 : row.Field<decimal>(dtColIndex));
                    footer.Text = totalDecimal.ToString("N03", new CultureInfo("en-us"));
                } else if (columnType == typeof(double)) {
                    Double totalDouble = dt.AsEnumerable().Sum(row => row.Field<double?>(dtColIndex) == null ? 0 : row.Field<double>(dtColIndex));
                    footer.Text = totalDouble.ToString("N03", new CultureInfo("en-us"));
                }
            }
        }

        /// <summary>
        /// Set the column's result sum
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="gv">GridView</param>
        /// <param name="dataTableColumnName">DataTable Column Name</param>
        /// <param name="position">Bottom[Default], Top</param>
        public static void setTotalColumn(DataTable dt, GridView gv, string dataTableColumnName, string position) {
            if (dt.Rows.Count > 0) {
                int dtColIndex = dt.Columns.IndexOf(dataTableColumnName);
                int gvColIndex = getColumnIndex(gv, dataTableColumnName);

                Type columnType = dt.Columns[dtColIndex].DataType;
                TableCell footer = gv.FooterRow.Cells[gvColIndex];
                TableCell header = gv.HeaderRow.Cells[gvColIndex];

                if (columnType == typeof(Int32)) {
                    int totalInt = dt.AsEnumerable().Sum(row => row.Field<int?>(dtColIndex) == null ? 0 : row.Field<int>(dtColIndex));
                    
                    if (position.ToLower() == "top") {
                        header.Text += "<br>" + totalInt.ToString("N03", new CultureInfo("en-us"));
                    } else {
                        footer.Text = totalInt.ToString("N03", new CultureInfo("en-us"));
                    }
                } else if (columnType == typeof(float) || columnType == typeof(decimal)) {
                    Decimal totalDecimal = dt.AsEnumerable().Sum(row => row.Field<decimal?>(dtColIndex) == null ? 0 : row.Field<decimal>(dtColIndex));

                    if (position.ToLower() == "top") {
                        header.Text += "<br>" + totalDecimal.ToString("N03", new CultureInfo("en-us"));
                    } else {
                        footer.Text = totalDecimal.ToString("N03", new CultureInfo("en-us"));
                    }
                } else if (columnType == typeof(double)) {
                    Double totalDouble = dt.AsEnumerable().Sum(row => row.Field<double?>(dtColIndex) == null ? 0 : row.Field<double>(dtColIndex));

                    if (position.ToLower() == "top") {
                        header.Text += "<br>" + totalDouble.ToString("N03", new CultureInfo("en-us"));
                    } else {
                        footer.Text = totalDouble.ToString("N03", new CultureInfo("en-us"));
                    }
                }
            }
        }

    /// <summary>
    /// Obtiene indice de una columna de un Gridview por nombre
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="colName"></param>
    /// <returns></returns>
    public static int getColumnIndex(GridView gv, string colName) {
            for (int i = 0; i < gv.Columns.Count; i++) {
                DataControlField field = gv.Columns[i];
                BoundField bfield = field as BoundField;

                if (bfield != null) {
                    if (bfield.DataField==colName) {
                        return i;
                    }
                }
            }
            return 0;
        }

        // Llena una tabla pasando parametros
        public static DataTable FillDataTable(string qry, CommandType cmdType, SqlParameter[] sqlParams) {
            String cnnStr = ConfigurationManager.ConnectionStrings["BaseDatosPpal"].ConnectionString;
            DataTable dt = new DataTable();

            try {
                using (SqlConnection cnn = new SqlConnection(cnnStr)) {
                    using (SqlCommand cmd = new SqlCommand(qry, cnn)) {
                        cmd.CommandType = cmdType;
                        cmd.Parameters.AddRange(sqlParams);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            } catch (Exception ex) {
                Console.Write(ex.Message);
                return dt;
            }    
        }

        // Llena una tabla sin pasar parametros
        public static DataTable FillDataTable(string qry, CommandType cmdType) {
            String cnnStr = ConfigurationManager.ConnectionStrings["BaseDatosPpal"].ConnectionString;
            DataTable dt = new DataTable();

            try {
                using (SqlConnection cnn = new SqlConnection(cnnStr)) {
                    using (SqlCommand cmd = new SqlCommand(qry, cnn)) {
                        cmd.CommandType = cmdType;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            } catch (Exception ex) {
                Console.Write(ex.Message);
                return dt;
            }    
        }
    }
}
