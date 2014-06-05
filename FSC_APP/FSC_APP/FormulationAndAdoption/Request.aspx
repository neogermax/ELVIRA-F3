<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/mpAdmin.master"
    CodeBehind="Request.aspx.vb" Inherits="FSC_APP.Request" Title="Solicitudes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <!-- Style -->
    <link href="../css/jquery-ui-1.10.4.customRequest.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap.min.css" />
    <link href="../css/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="../Pretty/css/prettyPhoto.css" rel="stylesheet" type="text/css" />
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
    <!-- End Style -->
    <!-- Script -->

    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>

    <script src="../js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <script src="../Pretty/js/jquery.prettyPhoto.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/bootstrap.min.js"></script>

    <script src="../Include/javascript/Request.js" type="text/javascript"></script>

    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>

    <script src="../Include/javascript/ThirdRequest.js" type="text/javascript"></script>

    <script src="../Include/javascript/FlowRequest.js" type="text/javascript"></script>

    <script src="../Include/javascript/jquery.maskMoney.js" type="text/javascript"></script>

    <script type="text/javascript">
        var idproject = <%= Request.QueryString("idproject") %>;
        $(document).ready(function(){
            $("input[type='text'], select, textarea").addClass("form-control");
            
            var tabs = $("#tabsRequest").tabs();
            
            //Execute load procedure for select third
            loadThird();
            //Set function for event change for select control
            SearchThirdInformation();
            //Set function for event change sum money values
            setEventChangeSum();
            
            //Load thirds in case add
            loadDataThirsProject();
            
            //Load Flows in case add
            loadPaymentFlowsByProject();
            
            //Load Details Flows in case add
            //loadPaymentDetailsFlowsByProject();
            
            //Process German
            validarporcentaje();
            
            $("#dialog").dialog({
                modal: true,
                minWidth: 700,
                closeOnEscape: true,
                autoOpen: false
            });
            
            //End Process German
            
            //mask controls money
            $('#ctl00_cphPrincipal_Txtvrdiner, #ctl00_cphPrincipal_Txtvresp, #txtTotalThird, .money').maskMoney({thousands: '.', decimal:',', precision: 0});
            //set datepicker control
            $("#ctl00_cphPrincipal_txtfechapago").datepicker();
            
            //New instance pretty photo
            $("a.pretty").prettyPhoto({
                callback: function() {
                    $.ajax({
                        url: "ajaxaddidea_drop_list_third.aspx",
                        type: "GET",
                        data: { "action": "cargarthird" },
                        success: function(result) {
                            $("#ddlactors").html(result);
                            $("#ddlactors").trigger("liszt:updated");
                        },
                        error: function()
                        { alert("los datos de terceros no pudieron ser cargados."); }
                    });
                }, /* Called when prettyPhoto is closed */
                ie6_fallback: true,
                modal: true,
                social_tools: false
            });
            
            $("#T_Actors").dataTable({
                "bJQueryUI": true,
                "bDestroy": true
            });
            
            $("#typeRequest").change(function(){            
            
                /*Adicion-prorroga-entregables*/
                if($(this).val() == "1"){
                    $("#group-radios-type").css("display", "block");
                    $("#divsuspension").css("display", "none");
                }
                else{
                    $("#group-radios-type").css("display", "none");
                    $("#tabsRequest").css("display", "none");
                }
                /*Suspension*/
                if($(this).val() == "2"){
                    $("#divsuspension").css("display", "block");
                    }else{
                    $("#divsuspension").css("display", "none");
                }
                /*Alcance*/
                if($(this).val() == "3"){
                    $("#divalcance").css("display","block");
                    }else{
                    $("#divalcance").css("display","none");
                }
                /*Cesion*/
                if($(this).val() == "4"){
                }else{
                }
                /*Otros*/
                if($(this).val() == "5"){
                    $("#divotros").css("display", "block");
                }else{
                    $("#divotros").css("display", "none");
                }
            });
            
            $("input[name='subgroup']").change(function(){
                $("#tabsRequest").css("display", "block");
                if($(this).val() != "Adición"){
                    tabs.tabs("disable",0);
                    tabs.tabs({ active: 1 });
                }else{
                    tabs.tabs("enable",0);
                    tabs.tabs({ active: 0 });
                }
            });
        });
    </script>

    <!-- End Script -->
    <div class="container-request">
        <h1 id="project-title">
        </h1>
        <div class="comun-controls">
            <h2 id="information-contract">
            </h2>
            <label>
                Tipo de solicitud</label>
            <select id="typeRequest">
                <option value="0">Seleccione...</option>
                <option value="1">1. Adición, Prórroga, Entregables</option>
                <option value="2">2. Suspensión</option>
                <option value="3">3. Alcance</option>
                <option value="4">4. Cesión</option>
                <option value="5">5. Otros</option>
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
        <div id="group-radios-type" class="container-subcategories" style="display: none;">
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
    <div id="tabsRequest" style="display: none;">
        <ul>
            <li><a href="#actores">Actores</a></li>
            <li><a href="#flujos">Flujos de Pago</a></li>
        </ul>
        <div id="actores">
            <ul>
                <li>
                    <label>
                        Actor</label>
                </li>
                <li>
                    <select id="ddlactors" class="">
                    </select>
                    <a id="linkactors" runat="server" href="~/GeneralPlanning/addThird.aspx?prety=1&op=add&iframe=true&width=100%&height=100%"
                        title="Nuevo actor" class="pretty">CREAR NUEVO ACTOR</a> </li>
                <li>
                    <asp:Label ID="Label6" runat="server" Text="Tipo"></asp:Label>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="">
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
                    <asp:TextBox ID="Txtvrdiner" runat="server" Width="200px" MaxLength="22" Rows="3"></asp:TextBox>
                    <asp:Label ID="Lblhelpdinner" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label13" runat="server" Text="Vr Especie"></asp:Label>
                    <asp:TextBox ID="Txtvresp" runat="server" Width="200px" MaxLength="22" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label14" runat="server" Text="Total Aporte del Actor"></asp:Label>
                    <input style="width: 0px; height: 0px; opacity: 0; position: absolute; z-index: 0px;"
                        type="text" id="txtTotalThird" value="" />
                    <br />
                    <asp:Label ID="Txtaportfscocomp" runat="server" Width="200px" Text="0"></asp:Label>
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
                    <br />
                    <input id="BtnaddActors" style="background-image: none;" type="button" value="Agregar Actor"
                        class="btn btn-success" name="Add_actors" onclick="return BtnaddActors_onclick()" />
                    <br />
                    <br />
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
                        <asp:TextBox ID="txtvalortotalflow" runat="server" Width="100px" MaxLength="5" onkeyup="this.value=this.value.replace(/[^\d]/,'')"></asp:TextBox>
                    </li>
                    <li width="25%">
                        <asp:Label ID="lblfechapago" runat="server" Text="Fecha de pago"></asp:Label>
                        <asp:TextBox ID="txtfechapago" runat="server" Width="100px" MaxLength="50"></asp:TextBox>
                        <input id="cesfechapago" value="" />
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
                        <br />
                        <input id="Btn_add_flujo" type="button" value="Agregar Pago" style="background-image: none;"
                            class="btn btn-success" name="Add_flujo" onclick="return Btn_add_flujo_onclick()" />
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
    <div id="dialog" title="Desembolso detallado">
        <table id="T_detalle_desembolso" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
            <thead>
                <tr>
                    <th>
                        No pago
                    </th>
                    <th>
                        Id aportante
                    </th>
                    <th>
                        Aportante
                    </th>
                    <th>
                        desembolso
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
                </tr>
            </tbody>
        </table>        
        <div>
            <input id="close_dialog" type="button" value="Cerrar X" name="close_dialog" />
        </div>
    </div>
    
            <div id="divsuspension" style="display: none;">
            <h2>
                Fechas de suspensión</h2>
            <br />
            <div>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblStartSuspend" runat="server" Text="Fecha de inicio de suspensión:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartSuspend" runat="server" MaxLength="50" Width="200px"></asp:TextBox><cc1:CalendarExtender
                                    ID="ceStartSuspend" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtStartSuspend">
                                </cc1:CalendarExtender>
                                <asp:CompareValidator ID="cvStartSuspend" runat="server" ControlToValidate="txtStartSuspend"
                                    ErrorMessage="aaaa/mm/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblEndSuspend" runat="server" Text="Fecha de fin de suspensión:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndSuspend" runat="server" MaxLength="50" Width="200px"></asp:TextBox><cc1:CalendarExtender
                                    ID="ceEndSuspend" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtEndSuspend">
                                </cc1:CalendarExtender>
                                <asp:CompareValidator ID="cvEndSuspend" runat="server" ControlToValidate="txtEndSuspend"
                                    ErrorMessage="aaaa/mm/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblInfoSuspend" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRestartType" runat="server" Text="Tipo de reinicio:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRestartType" runat="server" CssClass="Ccombo">
                                    <asp:ListItem Value="1">Reinicio automático</asp:ListItem>
                                    <asp:ListItem Value="2">Reinicio condicionado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div id="divalcance" style="display: none;">
            <h2>
                Información actual del proyecto:</h2>
            <div style="width: 45%; float: left;">
                <label>
                    Objetivo:</label>
                <textarea id="txtarObjective"></textarea>
                <label>
                    <input type="checkbox" id="chkObjective" onclick="mostrar('chkObjective');">
                    Modificar</label>
                <br />
                <label>
                    Objetivos Especificos:</label>
                <textarea id="txtarSpecificObjectives"></textarea>
                <label>
                    <input type="checkbox" id="chkSpecificObjectives" onclick="mostrar('chkSpecificObjectives');">
                    Modificar</label>
                <br />
                <label>
                    Resultados capacidad instalada:</label>
                <textarea id="txtarInstalledCapacityResults"></textarea>
                <label>
                    <input type="checkbox" id="chkInstalledCapacityResults" onclick="mostrar('chkInstalledCapacityResults');">
                    Modificar</label>
                <br />
                <label>
                    Resultados beneficiarios:</label>
                <textarea id="txtarBenefitiaryResults"></textarea>
                <label>
                    <input type="checkbox" id="chkBenefitiaryResults" onclick="mostrar('chkBenefitiaryResults');">
                    Modificar</label>
                <br />
                <label>
                    Resultados gestión del conocimiento:</label>
                <textarea id="txtarKnowledgeResults"></textarea>
                <label>
                    <input type="checkbox" id="chkKnowledgeResults" onclick="mostrar('chkKnowledgeResults');">
                    Modificar</label>
                <br />
                <label>
                    Otros resultados:</label>
                <textarea id="txtarOtherResults"></textarea>
                <label>
                    <input type="checkbox" id="chkOtherResults" onclick="mostrar('chkOtherResults');">
                    Modificar</label>
                <br />
                <label>
                    Obligaciones de las partes:</label>
                <textarea id="txtarPartObligations"></textarea>
                <label>
                    <input type="checkbox" id="chkPartObligations" onclick="mostrar('chkPartObligations');">
                    Modificar</label>
                <br />
            </div>
            <div style="width: 45%; float: right;">
                <label id="lblObjective2">
                    Objetivo:</label>
                <textarea id="txtarObjective2"></textarea>
                <br />
                <label id="lblSpecificObjectives2">
                    Objetivos Especificos:</label>
                <textarea id="txtarSpecificObjectives2"></textarea>
                <br />
                <label id="lblInstalledCapacityResults2">
                    Resultados capacidad instalada:</label>
                <textarea id="txtarInstalledCapacityResults2"></textarea>
                <br />
                <label id="lblBenefitiaryResults2">
                    Resultados beneficiarios:</label>
                <textarea id="txtarBenefitiaryResults2"></textarea>
                <br />
                <label id="lblKnowledgeResults2">
                    Resultados gestión del conocimiento:</label>
                <textarea id="txtarKnowledgeResults2"></textarea>
                <br />
                <label id="lblOtherResults2">
                    Otros resultados:</label>
                <textarea id="txtarOtherResults2"></textarea>
                <br />
                <label id="lblPartObligations2">
                    Obligaciones de las partes:</label>
                <textarea id="txtarPartObligations2"></textarea>
            </div>
        </div>
        <div id="divotros" style="display: none;">
            <h2>
                Solicitud</h2>
            <textarea id="txtarRequest"></textarea>
        </div>
    
    <input type='button' id="buttonSaveRequest" value="Guardar Solicitud" class="btn btn-success"
        style="background-image: none;" />
</asp:Content>
