<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/mpAdmin.master"
    CodeBehind="Request.aspx.vb" Inherits="FSC_APP.Request" Title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/bootstrap.min.css" />

    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>

    <script type="text/javascript" src="../js/bootstrap.min.js"></script>

    <script src="../Include/javascript/Request.js" type="text/javascript"></script>
    
    <link href="../css/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        var idproject = <%= Request.QueryString("idproject") %>;
        $(document).ready(function(){
            $("input[type='text'], select, textarea").addClass("form-control");
            
            $("#tabsRequest").tabs();
        });
    </script>

    <style>
        .container-request
        {
            padding: 1em 1em 1em 1em;
            color: #191919;
        }
        .container-request h1
        {
            font-size: 2em;
            width: 100%;
            text-align: center;
        }
        .comun-controls
        {
            margin-top: 3em;
            width: 49%;
            display: inline-block;
        }
        .container-request h2
        {
            font-size: 1.5em;
            width: 100%;
        }
        .comun-controls label
        {
            font-weight: bold;
            margin-top: 3em;
        }
        .comun-controls label, .comun-controls textarea, .comun-controls select
        {
            display: block;
            width: 80%;
            margin-bottom: 1em;
        }
        .container-subcategories
        {
            margin-top: 2em !important;
            clear: both;
            width: 100%;
        }
        .container-subcategories div
        {
            display: block;
        }
        .container-subcategories label, .container-subcategories input
        {
            /*display: inline-block;*/
        }
        .container-subcategories label
        {
            /*font-weight: bold;*/
        }
        .project-information
        {
            clear: both;
            margin-top: 2em;
        }
        .project-information label
        {
            font-weight: bold;
            margin-top: 2em;
        }
    </style>
    <div class="container-request">
        <h1 id="project-title">
        </h1>
        <div class="comun-controls">
            <h2 id="information-contract">
            </h2>
            <label>
                Tipo de solicitud</label>
            <select>
                <option>1. Adición, Prórroga, Entregables</option>
                <option>2. Suspensión</option>
                <option>3. Alcance</option>
                <option>4. Cesión</option>
                <option>5. Otros</option>
            </select>
        </div>
        <div class="comun-controls">
            <h2>
                <strong>No. de la Solicitud:</strong> <span id="numberRequest"></span>
            </h2>
            <h2 id="dateRequest">
            </h2>
            <label>
                Justificación de la solicitud</label>
            <textarea></textarea>
        </div>
        <div class="container-subcategories">
            <label>
                Especifique la modificación:</label>
            <div class="btn-group" data-toggle="buttons">
                <label class="btn btn-success">
                    <input type="radio" name="subgroup" value="Adición" />
                    Adición</label>
                <label class="btn btn-success">
                    <input type="radio" name="subgroup" value="Prorroga" />
                    Prorroga</label>
                <label class="btn btn-success">
                    <input type="radio" name="subgroup" value="Entregables" />
                    Entregables</label>
                <label class="btn btn-success">
                    <input type="radio" name="subgroup" value="Fecha de los desembolsos" />
                    Fecha de los desembolsos</label>
            </div>
        </div>
        <div class="project-information">
            <label>
                Información actual del proyecto (No Editable)</label>
            <label>
                Fecha de Incio: <span id="startDate"></span>
            </label>
            <label>
                Fecha de Cierre: <span id="closeDate"></span>
            </label>
            <label>
                Fecha de Cierre Proyecto Madre: <span id="closeDateMotherDate"></span>
            </label>
        </div>
    </div>
    <br />
    <br />
    <div id="tabsRequest">
        <ul>
            <li><a href="#actores">Actores</a></li>
            <li><a href="#flujos">Flujos de Pago</a></li>
        </ul>
        <div id="actores">
            <ul>
                <li>
                    <asp:Label ID="Label2" runat="server" Text="Actor"></asp:Label>
                </li>
                <li>
                    <select id="ddlactors" class="Ccombo">
                        <asp:DropDownList ID="ddlactors" runat="server">
                        </asp:DropDownList>
                    </select>
                    <a id="linkactors" runat="server" href="~/GeneralPlanning/addThird.aspx?prety=1&op=add&iframe=true&width=100%&height=100%"
                        title="Nuevo actor" class="pretty">CREAR NUEVO ACTOR</a> </li>
                <li>
                    <asp:Label ID="Label6" runat="server" Text="Tipo"></asp:Label>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="Ccombo">
                        <asp:ListItem>Operador</asp:ListItem>
                        <asp:ListItem>Socio Operador</asp:ListItem>
                        <asp:ListItem>Socio</asp:ListItem>
                        <asp:ListItem>Cliente</asp:ListItem>
                        <asp:ListItem>Contratante</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li id="li111" runat="server" visible="False">
                    <asp:Label ID="Label4" runat="server" Text="Aporte o rol"></asp:Label>
                    <asp:TextBox ID="txtActions" runat="server" Width="500px" MaxLength="1500" Rows="3"
                        TextMode="MultiLine"></asp:TextBox>
                </li>
                <li id="li112" runat="server" visible="False">
                    <asp:Label ID="Label5" runat="server" Text="Experiencias"></asp:Label>
                    <asp:TextBox ID="txtExperiences" runat="server" Width="500px" MaxLength="1500" Rows="3"
                        TextMode="MultiLine"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label9" runat="server" Text="Contacto"></asp:Label>
                    <asp:TextBox ID="Txtcontact" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label15" runat="server" Text="C.C"></asp:Label>
                    <asp:TextBox ID="Txtcedulacont" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label16" runat="server" Text="Teléfono"></asp:Label>
                    <asp:TextBox ID="Txttelcont" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label17" runat="server" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="Txtemail" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="vrdiner" runat="server" Text="Vr Dinero"></asp:Label>
                    <asp:TextBox ID="Txtvrdiner" runat="server" Width="200px" MaxLength="22" Rows="3"
                        onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                    <asp:Label ID="Lblhelpdinner" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label13" runat="server" Text="Vr Especie"></asp:Label>
                    <asp:TextBox ID="Txtvresp" runat="server" Width="200px" MaxLength="22" Rows="3" onkeyup="format(this)"
                        onchange="format(this)"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label14" runat="server" Text="Total Aporte del Actor"></asp:Label>
                    <asp:TextBox ID="Txtaportfscocomp" runat="server" Width="200px" MaxLength="30" Rows="3"
                        Enabled="False"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="LblinformationFlujo" runat="server" Text="Requiere flujo de pago:"></asp:Label>
                    <asp:RadioButtonList ID="RBListflujo" runat="server" Height="53px" RepeatDirection="Horizontal"
                        ValidationGroup="flujo" Width="86px">
                        <asp:ListItem Value="1">Si</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="Lblflujosinf" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Button ID="btnAddThird" Visible="false" runat="server" Text="Agregar Actor"
                        ValidationGroup="thirdBYIdea" />
                    <input id="BtnaddActors" type="button" value="Agregar Actor" name="Add_actors" onclick="return BtnaddActors_onclick()" />
                    <asp:Label ID="lblavertenactors" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                        ForeColor="Red"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblactorrep" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <div id="T_ActorsContainer">
                    <table id="T_Actors" align="center" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>
                                </th>
                                <th>
                                    Actores
                                </th>
                                <th>
                                    Tipo
                                </th>
                                <th>
                                    Contacto
                                </th>
                                <th>
                                    Documento Identidad
                                </th>
                                <th>
                                    Teléfono
                                </th>
                                <th>
                                    Correo electrónico
                                </th>
                                <th>
                                    Vr Dinero
                                </th>
                                <th>
                                    Vr Especie
                                </th>
                                <th>
                                    Vr total
                                </th>
                                <th>
                                    Eliminar
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <li>
                    <asp:GridView ID="gvThirds" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdactor" runat="server" Text='<%# Eval("idthird") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actores">
                                <ItemTemplate>
                                    <asp:Label ID="lblIname" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <asp:Label ID="lbltype" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contacto">
                                <ItemTemplate>
                                    <asp:Label ID="lblIcontact" runat="server" Text='<%# Eval("contact") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento Identidad">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdocument" runat="server" Text='<%# Eval("documents") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teléfono">
                                <ItemTemplate>
                                    <asp:Label ID="lblphone" runat="server" Text='<%# Eval("phone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo electrónico">
                                <ItemTemplate>
                                    <asp:Label ID="lblemail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vr Dinero">
                                <ItemTemplate>
                                    <asp:Label ID="lblMoney" runat="server" Text='<%# Eval("Vrmoney") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vr Especie">
                                <ItemTemplate>
                                    <asp:Label ID="lblespecies" runat="server" Text='<%# Eval("VrSpecies") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Actor">
                                <ItemTemplate>
                                    <asp:Label ID="lbltolfsc" runat="server" Text='<%# Eval("FSCorCounterpartContribution") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </li>
                <li id="totales" runat="server" visible="false">
                    <asp:Label ID="Label18" runat="server" Text=" Totales"></asp:Label>
                    <asp:Label Style="text-align: center;" ID="Txtsub1" runat="server" Width="105px"
                        BorderStyle="None"></asp:Label>
                    <asp:Label Style="text-align: center;" ID="Textsub2" runat="server" Width="105px"
                        BorderStyle="None"></asp:Label>
                    <asp:Label Style="text-align: center;" ID="Txtsub3" runat="server" Width="105px"
                        BorderStyle="None"></asp:Label>
                </li>
            </ul>
        </div>
        <div id="flujos">
            <div id="marco_1" style="border-top: solid; border-left: solid; border-right: solid;
                border-bottom: solid; border-color: white;" align="left">
                <br />
                <ul>
                    <li style="margin-left: 3em;">
                        <asp:Label ID="Lbltitleflujo1" runat="server" Text="PASO 1: Infomación general del desembolso"
                            Font-Bold="True" Font-Size="Large"></asp:Label>
                    </li>
                </ul>
                <ul id="listFlujosPagos">
                    <li width="25%">
                        <asp:Label ID="lblvalortotal" runat="server" Text="Pago No"></asp:Label>
                        <asp:TextBox ID="txtvalortotalflow" runat="server" Width="100px" MaxLength="50" onkeychange="ValidaSoloNumeros()"
                            onkeyup="ValidaSoloNumeros()" onkeypress="ValidaSoloNumeros()"></asp:TextBox>
                    </li>
                    <li width="25%">
                        <asp:Label ID="lblfechapago" runat="server" Text="Fecha de pago"></asp:Label>
                        <asp:TextBox ID="txtfechapago" runat="server" Width="100px" MaxLength="50"></asp:TextBox>
                        <input ID="cesfechapago" value="" />
                    </li>
                    <li width="25%">
                        <asp:Label ID="lblporcentaje" runat="server" Text="Porcentaje"></asp:Label>
                        <asp:TextBox ID="txtporcentaje" runat="server" MaxLength="50" Width="100px"></asp:TextBox>
                        <asp:Label ID="Lblhelpporcentaje" runat="server" ForeColor="#990000" Font-Bold="True"></asp:Label>
                    </li>
                    <li width="25%">
                        <asp:Label ID="lblvalor" runat="server" Text="Valor"></asp:Label>
                        <asp:Label ID="Lbltotalvalor" runat="server"></asp:Label>
                        <asp:TextBox ID="txtvalorpartial" runat="server" MaxLength="50" ReadOnly="true" Width="182px"
                            Visible="false"></asp:TextBox>
                    </li>
                </ul>
                <br />
                <asp:Label Style="clear: both; margin-left: 3em;" ID="lblentregable" runat="server"
                    Text="Entregable"></asp:Label>
                <asp:TextBox ID="txtentregable" runat="server" Height="100px" MaxLength="8000" TextMode="MultiLine"
                    Width="90%" Style="margin-left: 3em; margin-right: 7em;"></asp:TextBox>
                <asp:HiddenField ID="HDvalorpagoflujo" runat="server" />
                <asp:Label ID="Lblinformationexist" runat="server" ForeColor="#990000" Style="margin-left: 3em;
                    text-align: center;" Font-Bold="True"></asp:Label>
            </div>
            <br />
            <div id="marco_2" style="border-top: solid; border-left: solid; border-right: solid;
                border-bottom: solid; border-color: white;" align="left">
                <br />
                <ul>
                    <li style="margin-left: 3em;">
                        <asp:Label ID="Lbltitleflujo2" runat="server" Text="PASO 2: Ingrese el detalle por cada socio. "
                            Font-Bold="True" Font-Size="Large"></asp:Label>
                    </li>
                </ul>
                <div id="T_AflujosContainer" style="margin-left: 3em; margin-right: 3em">
                    <table id="T_Actorsflujos" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <thead>
                            <tr>
                                <th style="width: 1px">
                                </th>
                                <th>
                                    Aportante
                                </th>
                                <th>
                                    Valor total aporte
                                </th>
                                <th>
                                    Valor por programar
                                </th>
                                <th>
                                    Saldo por programar
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td style="width: 1px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <ul>
                    <li style="margin-left: 3em;">
                        <asp:Label ID="Lblinformation_flujos" runat="server" ForeColor="#990000"></asp:Label>
                    </li>
                    <li style="margin-left: 3em;">
                        <input id="Btn_add_flujo" type="button" value="Agregar pago" name="Add_flujo" onclick="return Btn_add_flujo_onclick()" />
                    </li>
                </ul>
                <br />
            </div>
            <br />
            <div id="marco_3" style="border-top: solid; border-left: solid; border-right: solid;
                border-bottom: solid; border-color: white;" align="left">
                <br />
                <ul>
                    <li style="margin-left: 3em;">
                        <asp:Label ID="Lblpaso3" runat="server" Text="Resumen programación desembolsos" Font-Bold="True"
                            Font-Size="Large"></asp:Label>
                    </li>
                </ul>
                <div id="T_flujosContainer" style="margin-left: 3em; margin-right: 3em">
                    <table id="T_flujos" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <thead>
                            <tr>
                                <th style="text-align: center;">
                                    No pago
                                </th>
                                <th style="text-align: center;">
                                    Fecha
                                </th>
                                <th style="text-align: center;">
                                    Porcentaje
                                </th>
                                <th style="text-align: center;">
                                    Entregable
                                </th>
                                <th style="text-align: center;">
                                    Valor parcial
                                </th>
                                <th style="text-align: center;">
                                    Editar/Eliminar
                                </th>
                                <th style="text-align: center;">
                                    Detalle
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
