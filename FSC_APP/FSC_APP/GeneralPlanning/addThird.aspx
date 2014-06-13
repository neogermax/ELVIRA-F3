<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    Inherits="FSC_APP.addThird" Title="addThird" CodeBehind="addThird.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script type="text/javascript" src="../Include/javascript/jquery-1.6.1.min.js"></script>

    <script src="../Include/javascript/third.js" type="text/javascript"></script>

    <link href="../css/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <link href="../css/elvira_F3.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //Función que permite solo Números
        function ValidaSoloNumeros() {
            if ((event.keyCode < 48) || (event.keyCode > 57))
                event.returnValue = false;
        }
        
        
    </script>

    <div id="containerSuccess" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="../images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblexit" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
        <asp:Label ID="lblexit2" runat="server" Style="font-size: 14pt; color: #FF0040;"></asp:Label>
    </div>
    <div id="tabsthird">
        <ul>
            <li style="margin-left: 15px;"><a href="#informacion">Información</a></li>
        </ul>
        <div id="informacion">
            <br />
            <div id="marco_1" style="border-top: solid; border-left: solid; border-right: solid;
                border-bottom: solid; border: 3px solid #9bbb58; background: #E8E8DC; border-radius: 15px;"
                align="left">
                <br />
                <ul>
                    <li style="margin-left: 15px;">
                        <asp:Label ID="Label9" runat="server" Text="Tipo de Persona"></asp:Label>
                        <select id="DDLtypepeople" class="Ccombo">
                            <asp:DropDownList ID="DDLtypepeople" runat="server">
                            </asp:DropDownList>
                        </select>
                        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                    </li>
                </ul>
                <div id="marco_datos_pricipal" style="border-top: solid; border-left: solid; border-right: solid;
                    border-bottom: solid; border: 3px solid #9bbb58; background: #FBFBEF; border-radius: 15px;
                    width: 95%; margin-left: 2%;" align="left">
                    <br />
                    <ul>
                        <li id="li_document_principal" style="margin-left: 15px;">
                            <asp:Label ID="Lbltipodocument1" runat="server" Text="Tipo de documento" CssClass="Ccombo"></asp:Label>
                            <asp:DropDownList ID="DDL_tipo_doc1" runat="server">
                                <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Cedula de ciudadania" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Cedula extranjera" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Pasaporte" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                        </li>
                        <li style="margin-left: 15px;">
                            <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                            <asp:TextBox ID="txtid" runat="server" Width="80%" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblHelpid" runat="server" Text=""></asp:Label>
                        </li>
                        <li style="margin-left: 15px;">
                            <asp:Label ID="lblcode" runat="server" Text="NIT (Ingresar sin guiones, puntos o espacios. Incluir dígito de verificación)"></asp:Label>
                            <asp:TextBox ID="txtcode" runat="server" Width="80%" MaxLength="50" onkeypress="ValidaSoloNumeros()"
                                onkeychange="ValidaSoloNumeros()" onkeyup="ValidaSoloNumeros()"></asp:TextBox>
                            <asp:Label ID="lblinf" runat="server"></asp:Label>
                            <asp:Label ID="lblHelpcode" runat="server" Text=""></asp:Label>
                        </li>
                        <li style="margin-left: 15px;">
                            <asp:Label ID="lblname" runat="server" Text="Nombre Actor"></asp:Label>
                            <asp:TextBox ID="txtname" runat="server" Width="80%" MaxLength="255" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtname"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblHelpname" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                        </li>
                        <li id="li_sex" style="margin-left: 15px;">
                            <asp:Label ID="Lblsex" runat="server" Text="Sexo"></asp:Label>
                            <asp:DropDownList ID="DDL_sex" runat="server">
                                <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Femenino" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Masculino" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="LblHELPSEX" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                        </li>
                    </ul>
                </div>
                <br />
                <div id="marco_datos_juridica" style="border-top: solid; border-left: solid; border-right: solid;
                    border-bottom: solid; border: 3px solid #9bbb58; background: #FBFBEF; border-radius: 15px;
                    width: 90%; margin-left: 5%;" align="left">
                    <br />
                    <ul>
                        <li style="margin-left: 40px;">
                            <asp:Label ID="Lbltitle1" runat="server" Text="Información del representante legal"
                                Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </li>
                        <li style="margin-left: 15px;">
                            <asp:Label ID="Lbltipodocument" runat="server" Text="Tipo de documento" CssClass="Ccombo"></asp:Label>
                            <asp:DropDownList ID="DDL_tipo_doc" runat="server">
                                <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Cedula de ciudadania" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Cedula extranjera" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Pasaporte" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                        </li>
                        <li style="margin-left: 15px;">
                            <asp:Label ID="Lbldocrep" runat="server" Text="N° documento"></asp:Label>
                            <asp:TextBox ID="Txtdocrep" runat="server" Width="80%" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                        </li>
                        <li style="margin-left: 15px;">
                            <asp:Label ID="lblreplegal" runat="server" Text="Representante legal"></asp:Label>
                            <asp:TextBox ID="Txtreplegal" runat="server" Width="80%" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                        </li>
                    </ul>
                </div>
                <br />
                <div id="marco_datos_contact" style="border-top: solid; border-left: solid; border-right: solid;
                    border-bottom: solid; border: 3px solid #9bbb58; background: #FBFBEF; border-radius: 15px;
                    width: 90%; margin-left: 5%;" align="left">
                    <br />
                    <ul>
                        <li style="margin-left: 40px;">
                            <asp:Label ID="Lbltitle2" runat="server" Text="Información de Contacto" Font-Bold="True"
                                Font-Size="Medium"></asp:Label>
                        </li>
                        <li id="li_contact" style="margin-left: 15px;">
                            <asp:Label ID="Lblcontact" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="Txtcontact" runat="server" Width="80%" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        </li>
                        <li id="li_actions" style="margin-left: 15px;">
                            <asp:Label ID="Lblactions" runat="server" Text="Número de identificación"></asp:Label>
                            <asp:TextBox ID="Txtactions" runat="server" Width="80%" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </li>
                        <li id="li_phone" style="margin-left: 15px;">
                            <asp:Label ID="Lblphone" runat="server" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="Txtphone" runat="server" Width="80%" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                        </li>
                        <li id="li_direccion" style="margin-left: 15px;">
                            <asp:Label ID="Lbldireccion" runat="server" Text="Dirección de Domicilio"></asp:Label>
                            <asp:TextBox ID="Txtdireccion" runat="server" Width="80%" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="Lblhelpdireccion" runat="server" Text=""></asp:Label>
                        </li>
                        <li id="li_mail" style="margin-left: 15px;">
                            <asp:Label ID="Lblmail" runat="server" Text="Correo Electrónico"></asp:Label>
                            <asp:TextBox ID="Txtemail" runat="server" Width="80%" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                        </li>
                    </ul>
                </div>
                <br />
                <ul>
                    <li style="margin-left: 15px;">
                        <%--<input id="SaveActors" type="button" value="Crear Actor" name="Save_Actors" />--%>
                        <a id="linkcancelar" runat="server" href="~/GeneralPlanning/searchThird.aspx" title="cancelar"
                            style="height: 2em;">Cancelar</a>
                        <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" OnClick="btnAddData_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="Guardar" />
                        <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False"
                            Visible="false" />
                    </li>
                    <li id="t1" runat="server" visible="false">
                        <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                        <asp:DropDownList ID="ddlenabled" runat="server">
                            <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                            <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblHelpenabled" runat="server" Text=""></asp:Label>
                    </li>
                    <li style="margin-left: 15px;">
                        <asp:Label ID="lbliduser" runat="server" Text="Usuario creación"></asp:Label>
                        <asp:TextBox ID="txtiduser" runat="server" Width="80%" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
                    </li>
                    <li style="margin-left: 15px;">
                        <asp:Label ID="lblcreatedate" runat="server" Text="Fecha creación"></asp:Label>
                        <asp:TextBox ID="txtcreatedate" runat="server" Width="80%" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
                    </li>
                    <li style="margin-left: 15px;">
                        <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                        <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                        <asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                            ForeColor="Red"></asp:Label><asp:HiddenField ID="HFswchit" runat="server" />
                        <asp:HiddenField ID="HFpretty" runat="server" />
                        <asp:HiddenField ID="HFperson" runat="server" />
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
