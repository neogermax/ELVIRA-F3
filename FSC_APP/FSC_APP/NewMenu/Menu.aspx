<%@ Page Language="VB" AutoEventWireup="false" Inherits="FSC_APP.NewMenu_Menu" Codebehind="Menu.aspx.vb" %>

<%@ Register Src="../Controls/User.ascx" TagName="User" TagPrefix="uc1" %>
<!DOCTYPE html />
<html>
<head>
    <title>Menu FSC</title>
    <link rel="stylesheet" type="text/css" href="../css/bootstrap.min.css" />
 
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>

    <link href="../Include/javascript/jfm/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Include/javascript/jfm/styles/jflow.style.css" rel="stylesheet" type="text/css" />

    <script src="../Include/javascript/jfm/jflow.plus.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/bootstrap.min.js"></script>

    <script src="../Include/javascript/login.js" type="text/javascript"></script>

    <style type="text/css">
        body
        {
            background-image: url(/App_Themes/GattacaAdmin/Images/Template/V9/bg.jpg);
            background-repeat: repeat-x;    
            color: #3D3D3D;
            font-family: Tahoma, Geneva, sans-serif;
            overflow: scroll;
            /*background-position-y:-84px;*/
        }
        
        .menuprincipal
        {
            width: 90%;
            margin: 3em auto;
            text-align: center;
        }
        .tarjeta img
        {
            width: 90%;
            margin-top: 0.5em;
        }
        .tarjeta
        {
            text-align: center;
            cursor: pointer;
            width: 150px;
            height: 150px;
            background: #FFFFFF;
            border: px solid #000000;
            display: inline-block;
            transition: all 0.2s; /*margin-top: 1em;*/
            margin-right: 1em;
            -moz-border-radius: 160px;
            -webkit-border-radius: 160px;
            border-radius: 0px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
            -moz-box-shadow: 0px 0px 4px #000000;
            -webkit-box-shadow: 0px 0px 4px #000000;
            box-shadow: 0px 0px 4px #000000; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS*/
        }
        .visible
        {
            display: none;
        }
        .tarjeta:hover
        {
            background: #75852F;
            -moz-border-radius: 0px;
            -webkit-border-radius: 0px;
            border-radius: 0px;
        }
        .tarjeta:activate
        {
            -moz-border-radius: 0px;
            -webkit-border-radius: 0px;
            border-radius: 0px;
        }
        .submenu
        {
            width: 100%;
            text-align: center;
        }
        .submenu ol
        {
            margin: 4em auto 0 0;
            padding: 0px 0px 0px 0px;
            width: 100%;
            color: #6e6e6e;
            text-align: center;
        }
        .submenu ol li:hover
        {
            color: #006B66;
            font-weight: bold;
            text-decoration: underline;
        }
        .submenu ol li:activate
        {
            color: #3D3D3D;
            font-weight: bold;
        }
        .submenu ol li:click
        {
            color: #3D3D3D;
            font-weight: bold;
        }
        .submenu ol li
        {
            display: inline-block;
            font-weight: bold;
            width: 10%;
            margin: 0 auto;
            cursor: pointer;
            color: #006B66;
            border-bottom: 1px solid #FBBA42;
            padding-bottom: 1em;
            font-size: 1em;
        }
        /* iPads (portrait) ----------- */
        @media only screen 
        and (min-device-width : 768px) 
        and (max-device-width : 1024px){
        /* Styles */
            .submenu ol li
            {
                display: inline-block;
                font-weight: bold;
                width: 12%;
                margin: 0 auto;
                cursor: pointer;
                color: #006B66;
                border-bottom: 1px solid #FBBA42;
                padding-bottom: 1em;
                font-size: 0.8em !important;
            }
        }
        .submenu_dos
        {
            width: 80%;
            margin: 3em auto;
        }
        .submenu_dos ol li
        {
            height: 60px;
            font-weight: bold;
            color: #EF9120;
            border-bottom: 1px solid #EF9120;
            line-height: 80px;
        }
        .submenu_dos ol li a
        {
            color: #006B66;
            font-weight: bold;
        }
        .tablasombra
        {
            -webkit-box-shadow: 2px 2px 5px #999;
            -moz-box-shadow: 2px 2px 5px #999;
        }
        
        #jFlowSlider
        {
        	margin-top: 7em !important;
        }
        #nav-menu
        {
        	width: 100%;
        }
        #nav-menu li
        {
        	display: inline;
        	float: right !important;
        	color: rgb(0, 128, 128);
        	height: 75px !important;
        	line-height: 75px !important;
        	text-align: center;
        }
    </style>

    <script type="text/javascript">
        var host = "<%= Request.Url.Authority %>";
        $(function () {

            $("#myController").jFlow({

                controller: ".jFlowControl", // must be class, use . sign

                slideWrapper: "#jFlowSlider", // must be id, use # sign

                slides: "#mySlides",  // the div where all your sliding divs are nested in

                selectedWrapper: "jFlowSelected",  // just pure text, no sign

                effect: "flow", //this is the slide effect (rewind or flow)

                width: "100%",  // this is the width for the content-slider

                height: "318px",  // this is the height for the content-slider

                duration: 400,  // time in milliseconds to transition one slide

                pause: 5000, //time between transitions

                prev: ".jFlowPrev", // must be class, use . sign

                next: ".jFlowNext", // must be class, use . sign

                auto: true

            });
            
            $(".cssBgBPO").css("padding", "0px 0px 0px 0px");

        });

    </script>

    <script type="text/javascript">
        var arrayLevel1 = null;
        var arrayLevel2 = null;
        var arrayLevel3 = null;
        var arrayLevel4 = null;
        var parent = "";
        var child = "";
        var jsonMenu = {
            "administrador": {
                "items": ["esecurity", "Panel General de Proyectos", "Planeación General ", "Empresa", "Reportes"],
                "esecurity": [["e-Security", "/V9/eSecurity/Default.aspx"], ["Menus", ""]],
                "Panel_General_de_Proyectos": [["Tipo Documento", ""]],
                /*"Admin_Procesos_BPM": [["Destinatario", ""]],*/
                "Empresa": [["Nueva Empresa", "/GeneralPlanning/addENTERPRISE.aspx?op=add"], ["Buscar Empresa", "/GeneralPlanning/searchENTERPRISE.aspx"]],
                "Componente": [["Nuevo Componente", "/FormulationAndAdoption/addComponent.aspx?op=add"], ["Buscar Componente", "/FormulationAndAdoption/searchcomponent.aspx"]],
                "Menus": [["Buscar Item", "/Administrator/AdminMenu.aspx"]],
                "Destinatario": [["Nuevo Destinatario", "/Execution/addAddressee.aspx?op=add"], ["Buscar Destinatario", "/Execution/searchAddressee.aspx"]],
                "Planeación_General_": [["Perspectiva", ""], ["Objetivo Estrategico", ""], ["Gerencia", ""], ["Estrategia", ""], ["Linea estrategica o Linea de Acción", ""], ["Programa", ""], ["Componentes de programa", ""], ["Actividad de la estrategia", ""], ["Actor", ""], ["Componente",""]],
                "Reportes": [["Planeación General", ""], ["Investigación y Desarrollo", ""], ["Convocatorias", ""], ["Consulta general de proyectos", "/Report/generalConsultingProjects.aspx"]],
                "Perspectiva": [["Nueva Perspectiva", ""], ["Buscar Perspectiva", ""]],
                /*"Objeto": [["Nueva Perspectiva", ""], ["Buscar Perspectiva", ""]],*/
                "Objetivo_Estrategico": [["Nuevo Objetivo Estrategico", "/GeneralPlanning/addSTRATEGICOBJECTIVE.aspx?op=add"], ["Buscar Objetivo Estrategico", "/GeneralPlanning/searchSTRATEGICOBJECTIVE.aspx"]],
                "Gerencia": [["Nuevo Gerencia", "/GeneralPlanning/addManagement.aspx?op=add"], ["Buscar Gerencia", "/GeneralPlanning/searchMANAGEMENT.aspx"]],
                "Estrategia": [["Nuevo Estrategia", "/GeneralPlanning/addSTRATEGY.aspx?op=add"], ["Buscar Estrategia", "/GeneralPlanning/searchSTRATEGY.aspx"]],
                "Linea_estrategica_o_Linea_de_Acción": [["Nueva Linea Estrategica", "/GeneralPlanning/addStrategicLine.aspx?op=add"], ["Buscar Linea Estrategica", "/GeneralPlanning/searchStrategicLine.aspx"]],
                "Programa": [["Nueva Programa", "/GeneralPlanning/addProgram.aspx?op=add"], ["Buscar Programa", "/GeneralPlanning/searchProgram.aspx"]],
                "Componentes_de_programa": [["Nuevo Componente de Programa", "/GeneralPlanning/addProgramComponent.aspx?op=add"]],
                "Actividad_de_la_estrategia": [["Nuevo Actividad de la estrategia", "/GeneralPlanning/addStrategicActivity.aspx?op=add"], ["Buscar Actividad de la estrategia", "/GeneralPlanning/searchStrategicActivity.aspx"]],
                "Actor": [["Nuevo Actor", "/GeneralPlanning/addThird.aspx?op=add"], ["Buscar Actor", "/GeneralPlanning/searchThird.aspx"]],
                "Planeación_General": [["Plan de Acción General", ""], ["Detalle Estrategia", ""], ["Detalle Linea Estrategica", ""], ["Inventario de Indicadores", ""], ["Gantt de Actividades de Estrategia", ""], ["Actor", ""]],
                "Investigación_y_Desarrollo": [["Inventario de Ideas", ""], ["Mapa de Actores", ""]],
                "Convocatorias": [["Estado de Convocatorias", ""], ["Lista de Propuestas", ""]],
                "Tipo_Documento": [["Nuevo Tipo Documento", "/Engagement/addDocumentType.aspx?op=add"], ["Buscar Tipo Documento", "/Engagement/searchDocumentType.aspx"]]
            },
            "formulacion": {
                "items": ["Idea", "Proyecto", "Contratación"],
                "Idea": [["Nueva Idea", "/ResearchAndDevelopment/addIdea.aspx?op=add"], ["Buscar Idea", "/ResearchAndDevelopment/searchIdea.aspx"], ["Aprobar Idea", "/FormulationAndAdoption/addProjectApprovalRecord.aspx?op=add"], ["Buscar aprobación de ideas", "/FormulationAndAdoption/searchProjectApprovalRecord.aspx"]],
//              "Aprobación": [, ["Consulta el registro de aprobación de una Idea", "/FormulationAndAdoption/searchProjectApprovalRecord.aspx"]]
                "Proyecto": [["Crear Proyecto", "/FormulationAndAdoption/addProject.aspx?op=add"], ["Buscar Proyecto", "/FormulationAndAdoption/searchProject.aspx"], ["Aprobar Proyecto", "/FormulationAndAdoption/addProjectApproval.aspx"], ["Plan de ejecución Contractual", ""]],
                "Contratación": [["Generar Contrato", "/Engagement/addContractRequest.aspx?op=add"], ["Buscar Contrato", "/Engagement/searchContractRequest.aspx"]],
                "Plan_de_ejecución_Contractual": [["Nuevo Registrar plan de ejecucion Contractual", "/OperationalPlanning/addExecutionContractualPlanRegistry.aspx?op=add"], ["Buscar Registrar plan de ejecucion Contractual", "/OperationalPlanning/addExecutionContractualPlanRegistry.aspx?op=add"]]
                //"Solicitud_de_contrato": [["Nueva Solicitud de contrato", "/Engagement/addContractRequest.aspx?op=add"], ["Buscar Solicitud de contrato", "/Engagement/searchContractRequest.aspx"]],
                //"Ejecución_de_contrato": [["Nueva Ejecucion de contrato", "/Engagement/addContractExecution.aspx?op=add"], ["Buscar Ejecucion de contrato", "/Engagement/searchContractExecution.aspx"]],
                //"Consulta_General_de_Proyectos": [["Consulta General de Proyectos", "/FormulationAndAdoption/searchProject.aspx"]]
            },
            "ejecucion": {
                "items": ["Inicio Proyecto", "Cronograma del Proyecto", "Indicador", "Acta de Seguimiento", "Gestion del Riesgo", "Ejecución Operadores", "Cierre del Proyecto", "Evaluación", "Reportes"],

                "Inicio_Proyecto": [["Generar Acta de Inicio", "/FomsProceedings/Proceeding_main.aspx?tp=str"]],
                "Cronograma_del_Proyecto": [["Ingresar Cronograma", "/FormulationAndAdoption/addproyectchargemasive.aspx"], ["Editar Cronograma", ""], ["Actividad", ""], ["Subactividad", ""]],
                "Actividad": [["Nueva Actividad", "/FormulationAndAdoption/addActivity.aspx?op=add"], ["Buscar Actividad", "/FormulationAndAdoption/searchActivity.aspx"]],
                "Subactividad": [["Nueva SubActividad", "/FormulationAndAdoption/addSubActivity.aspx?op=add"], ["Buscar SubActividad", "/FormulationAndAdoption/searchSubActivity.aspx"]],

                "Indicador": [["Nuevo Indicador", "/GeneralPlanning/addIndicator.aspx?op=add"], ["Buscar Indicador", "/GeneralPlanning/searchIndicator.aspx"], ["Objetivo", ""]],
                "Objetivo": [["Nuevo Objetivo", "/FormulationAndAdoption/addObjective.aspx?op=add"], ["Buscar Objetivo", "/FormulationAndAdoption/searchObjective.aspx"]],
                "Acta_de_Seguimiento": [["Generar Acta de Seguimiento", "/FomsProceedings/Proceeding_main.aspx?tp=trc"]],
                "Gestion_del_Riesgo": [["Nuevo Riesgo (Incluye toda la clasificación y la acción de mitigación)", "/FormulationAndAdoption/addRisk.aspx?op=add"], ["Busqueda (incluye busqueda por toda la clasificacion)", "/FormulationAndAdoption/searchRisk.aspx"], ["Mitigación", ""]],
                "Mitigación": [["Nueva Mitigación", "/FormulationAndAdoption/addMitigation.aspx?op=add"], ["Buscar Mitigación", "/FormulationAndAdoption/searchMitigation.aspx"]],
                "Ejecución_Operadores": [["Registrar Testimonios y Aprendizajes", "/Execution/addExecution.aspx?op=add"], ["Listado de subactividades pendientes", "/Execution/subActivityMainPanelTODO-LIST.aspx"], ["Listado de actividades pendientes", "/Execution/addSubActivityInformationRegistry.aspx?op=add&idsubactivity=2593"], ["Listado de ajustes pendientes", "/V9/BPM/InternalParticipant/TaskList.aspx"]],
                "Cierre_del_Proyecto": [["Generar Acta de Cierre", "/FomsProceedings/Proceeding_main.aspx?tp=cls"], ["Lecciones Aprendidas", ""],["Evaluación Proveedor", ""]],
                "Registro_de_Cierre": [["Nuevo Registro de Acta de cierre", ""], ["Buscar Registro de Acta de cierre", ""]],
                "Lecciones_Aprendidas": [["Registrar testimonios ajustes y aprendizajes", "/Execution/addExecution.aspx?op=add"], ["Busqueda (incluida busqueda por toda la clasificacion)", "/Execution/searchExecution.aspx"]],

                //"Evaluación": [["Evaluación Proveedor", ""]],
                "Evaluación_Proveedor": [["Nueva Evaluacion Proveedor", "/ResearchAndDevelopment/addSupplierEvaluation.aspx?op=add"], ["Buscar Evaluacion Proveedor", "/ResearchAndDevelopment/searchSupplierEvaluation.aspx"]],

                "Reportes": [["Formulacion y Planeación", ""], ["Ejecución", ""], ["Reportes de Contratación", ""], ["Reporte de Actas", "/Report/Engagement/ReportContractRequestactas.aspx"], ["Reporte de Compromisos", "/Report/proceedings/ReportCompromise.aspx"]],
                "Reportes_de_Contratación": [["Estado de la solicitud de contratos", "/Report/Engagement/reportContractsRequest.aspx"], ["Listado general de contratos", "/Report/Engagement/generalListContracts.aspx"], ["Plan de contratacion", "/Report/OperationalPlanning/reportRecruitmentPlan.aspx"]],
                "Formulacion_y_Planeación": [["Datos basicos de Proyecto", "/Report/FormulationAndAdoption/reportBasicProjectData.aspx"], ["Matriz de Indicadores", "/Report/FormulationAndAdoption/reporMatrixIndicator.aspx"], ["Plan de Ejecución", "/Report/reportExecutionPlan.aspx"], ["Cronograma del proyecto", "/Report/FormulationAndAdoption/reportProjectChronogram.aspx"], ["Matriz de Riesgos", "/Report/FormulationAndAdoption/reportRiskMatrix.aspx"], ["Plan de contratación", "/Report/OperationalPlanning/reportRecruitmentPlan.aspx"]],
                "Ejecución": [["Lista de Subactividades", "/Report/Execution/reportActivities.aspx"], ["Banco de documentos", "/Report/ResearchAndDevelopment/ReportDocuments.aspx"], ["Productos de Proyecto", "/Report/ResearchAndDevelopment/ReportProducts.aspx"], ["Listado de buenas practicas", "/Report/ResearchAndDevelopment/ReportGoodPractice.aspx"], ["Actividades Vs Ejecución", "/Report/FormulationAndAdoption/ReportSubactivitiesVsExecution.aspx"], ["Estadisticas", "/Report/ResearchAndDevelopment/ReportStatistics.aspx"], ["Aprendizaje Logros y Ajustes", "/Report/FormulationAndAdoption/reportlearning.aspx"], ["Lista de testimonios", "/Report/FormulationAndAdoption/ReporTestimonyList.aspx"], ["Grafica de Encuestas", "/Report/ResearchAndDevelopment/ResultInquest.aspx"], ["Lista de Operadores", "/Report/FormulationAndAdoption/ReportOperatorList.aspx"], ["Lista de proyectos cerrados", "/Report/FormulationAndAdoption/reportClosedProjectsList.aspx"]] 
                
            },
            "herramientas": {
                "items": ["Encuesta", "Documentos", "Foro"],
                "Encuesta": [["Nueva Encuesta", "/Execution/addInquest.aspx?op=add"], ["Buscar Encuesta", "/Execution/searchInquest.aspx"], ["Diligenciar Encuesta", ""], ["Buscar Encuesta Resuelta", "/Execution/searchResolvedInquest.aspx"], ["Nuevo Contenido Encuesta", "/Execution/addInquestContent.aspx?op=add"], ["Buscar contenido encuesta", "/Execution/searchInquestContent.aspx"]],
                "Diligenciar_Encuesta": [["Buscar", "/Execution/searchResolvedInquest.aspx"]],
                "Documentos": [["Nuevo Documento", "/ResearchAndDevelopment/addDocuments.aspx?op=add"], ["Buscar Documento", "/ResearchAndDevelopment/searchDocuments.aspx"]],
                "Foro": [["Foro del Proyecto", "/FormulationAndAdoption/searchForum.aspx"], ["Nuevo Foro", "/FormulationAndAdoption/addForum.aspx?op=add"]]
            }
        };

        $(document).ready(function() {
        
            $(".tarjeta").hover(function() {
                var menuText = $(this).attr("menu");
                $(".titlemenu").html(menuText);
                $(".titlemenu").css("font-size", "1em");
                $(".titlemenu").css("font-weight", "bold");
                $(".titlemenu").css("text-transform", "uppercase");
                $(".titlemenu").css("margin-top", "20px");
            });

            $(".tarjeta, .tarjeta img").click(function() {
                Minimizar_MenuPrincipal($(this)[0]);
            });

            $(".tarjeta img").hover(function() {
                var menuText = $(this).parent().attr("menu");
                $(".titlemenu").html(menuText);
            });
            $("body").hover(function() {
                $(".titlemenu").html("");
            });
            $.ajax({
                url: "/NewMenu/ajaxMenu.aspx",
                type: "GET",
                data: { "action": "loadmenu", "level": "1"},
                success: function(result){
                    arrayLevel1 = result.split(",");
                    for(var item in arrayLevel1){
                        var itemArray = $.trim(arrayLevel1[item]).toString();
                        console.log(arrayLevel1[item]);
                        if(itemArray != ""){
                            console.log($("#" + itemArray));
                            $("#" + itemArray).removeClass('visible');
                        }
                    }
                }
            });
            
        });
        function Minimizar_MenuPrincipal(objDOM) {
            $(".menuprincipal").css("margin", "1em auto");
            $(".tarjeta img").css("margin", "0.2em auto");
            $(".tarjeta").css("height", "50px").css("width", "50px");
            $(".submenu").css("display", "block");
            $(".titlemenu").css("font-size", "1em");
            $(".titlemenu").css("color", "#3D3D3D");
            $(".titlemenu").css("font-weight", "bold");
            $(".titlemenu").css("text-transform", "uppercase");
            var nombremenu = $(objDOM).attr("item");
            parent = nombremenu;
            $("#listsubmenu").html("");
            for (var itemMenu in jsonMenu[nombremenu].items) {
                console.log(jsonMenu[nombremenu][itemMenu]);
                if (arrayLevel2.indexOf(jsonMenu[nombremenu].items[itemMenu])!=-1){
                    $("#listsubmenu").append("<li onclick='mostrarTercerNivelMenu(this);' item='" + jsonMenu[nombremenu].items[itemMenu] + "'>" + jsonMenu[nombremenu].items[itemMenu] + "</li>");
                }
            }
        }
        function mostrarTercerNivelMenu(objDOM) {
            $(".submenu_dos").css("display", "block");
            $('html, body').animate({
                scrollTop: 550
            }, 500);
            var nombremenu = $(objDOM).attr("item");
            nombremenu = nombremenu.replace(/ /g, "_");
            $("#listsubmenu_dos").html("");
            child = nombremenu;
            for (var itemMenu in jsonMenu[parent][nombremenu]) {
                console.log(jsonMenu[parent][nombremenu][itemMenu][0, 0]);
                if (arrayLevel3.indexOf(jsonMenu[parent][nombremenu][itemMenu][0, 0])!=-1){
                    if (jsonMenu[parent][nombremenu][itemMenu][0, 1] == "") {
                        $("#listsubmenu_dos").append("<li><a title='Opciones de Menu' data-toggle='modal' href='#menuModal' onclick='mostrarCuartoNivelMenu(this);' item='" + jsonMenu[parent][nombremenu][itemMenu][0, 0].replace(/ /g, "_") + "'>" + jsonMenu[parent][nombremenu][itemMenu][0, 0] + "</a><a onclick='mostrarCuartoNivelMenu(this);' item='" + jsonMenu[parent][nombremenu][itemMenu][0, 0].replace(/ /g, "_") + "' title='Mas Opciones ...' data-toggle='modal' href='#menuModal' > <img src='../images/Folder.png' style='width: 32px; float: right; margin-top: 15px;' /></a></li>");
                    }
                    else {
                        $("#listsubmenu_dos").append("<li><a href='" + jsonMenu[parent][nombremenu][itemMenu][0, 1].replace(/ /g, "_") + "'>" + jsonMenu[parent][nombremenu][itemMenu][0, 0] + "</a></li>");
                    }
                }
            }
        }
        function mostrarCuartoNivelMenu(objDOM) {
            var nombremenu = $(objDOM).attr("item");
            $("#listModalMenu").html("");
            for (itemMenu in jsonMenu[parent][nombremenu]) {
                if (arrayLevel4.indexOf(jsonMenu[parent][nombremenu][itemMenu][0, 0])!=-1){
                    $("#listModalMenu").append("<li><a href='" + jsonMenu[parent][nombremenu][itemMenu][0, 1] + "'>" + jsonMenu[parent][nombremenu][itemMenu][0, 0] + "</a></li>");
                }
            }
        }
    </script>

</head>
<body>
    <div id="container" style="margin: 63px auto;">
        <div id="sliderContainer">
            <div id="mySlides">
                <div id="slide1" class="slide">
                    <img src="../Include/javascript/jfm/images/01.jpg" alt="" />
                    <!-- <div class="slideContent">
                    <h3>You Asked, jFlow Delivered</h3>
					<strong>It's all about the Community and giving back. To keep with this tradition, jFlow Plus now has more of the features you want.</strong>
                    <p>It's all about the Community and giving back. To keep with this tradition, jFlow Plus now has more of the features you want.</p>
                </div> -->
                </div>
                <div id="slide2" class="slide">
                    <img src="../Include/javascript/jfm/images/02.jpg" alt="" />
                    <!-- <div class="slideContent">
                    <h3>W3C Valid</h3>
					<p><strong>Are you a stickler for writing valid code? So is jFlow. Run this puppy through W3C's validator to see it pass the test!</strong></p>
                    <p>Are you a stickler for writing valid code? So is jFlow. Run this puppy through W3C's validator to see it pass the test!</p>
                </div> -->
                </div>
                <div id="slide3" class="slide">
                    <img src="../Include/javascript/jfm/images/03.jpg" alt="" />
                    <!-- <div class="slideContent">
                    <h3>Frequent Code Updates</h3>

                    <p><strong>This slider is actively developed and used by thousands of websites. More features coming soon including more effects and options.</strong></p>
                    <p>This slider is actively developed and used by thousands of websites. More features coming soon including more effects and options.</p>
                </div> -->
                </div>
                <!--   <div id="slide4" class="slide"><img src="images/jflow-sample-slide4.jpg" alt="Slide 3 jFlow Plus"/>

                <div class="slideContent">
                    <h3>Notice the Slide Navigation?</h3>

                    <p><strong>That's a new feature. Click on the paging buttons in the top-right to quickly jump to any jFlow slide number.</strong></p>
                    <p>That's a new feature. Click on the paging buttons in the top-right to quickly jump to any jFlow slide number.</p>
                </div>
            </div>-->
            </div>
            <div id="myController">
                <span class="jFlowControl"></span><span class="jFlowControl"></span><span class="jFlowControl">
                </span>
            </div>
            <div class="jFlowPrev">
            </div>
            <div class="jFlowNext">
            </div>
        </div>
    </div>
    <div style="margin: 7em auto;">
        <table border="2" bordercolor="#9BBB59" style="width: 85%; margin: 0 auto;" class="tablasombra">
            <tbody>
                <tr>
                    <td style="border: solid 1px; border-color: #FFFFFF;">
                        <p style="text-align: center; font-size: 28px; color: #499591;">
                            Bienvenidos al Sistema de Gestión de Proyectos</p>
                        <p style="text-align: center; font-size: 30px; color: #499591;">
                            ELVIRA</p>
                        <p style="text-align: center; color: #499591;  margin-top: 1em;">
                            <em><strong>E</strong>va<strong>L</strong>uaci&oacute;n y <strong>V</strong>aloraci&oacute;n
                                de la <strong>I</strong>nve<strong>R</strong>si&oacute;n <strong>A</strong>rticulada</em></p>
                        <div class="menuprincipal">
                            <div id="administrador" item="administrador" menu="Administración" class="tarjeta visible">
                                <img src="../images/IDCard.png" alt="Administracion" />
                            </div>
                            <div id="formulacion" item="formulacion" menu="Formulación y planeación" class="tarjeta visible">
                                <img src="../images/Flipchart.png" alt="Formulación y planeación" />
                            </div>
                            <div id="ejecucion" item="ejecucion" menu="Ejecución y cierre de proyectos" class="tarjeta visible">
                                <img src="../images/0009_Calendar.png" alt="Ejecución y cierre de proyectos" />
                            </div>
                            <div id="herramientas" item="herramientas" menu="Herramientas" class="tarjeta visible">
                                <img src="../images/0008_Bullseye.png" alt="Herramientas" />
                            </div>
                            <h1 class="titlemenu">
                            </h1>
                        </div>
                        <div style="display: none;" class="submenu">
                            <ol id="listsubmenu">
                            </ol>
                        </div>
                        <div style="display: none;" class="submenu_dos">
                            <ol id="listsubmenu_dos">
                            </ol>
                        </div>
                        <div id="menuModal" class="modal fade">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                        </button>
                                        <h4 class="modal-title">
                                            Mas Opciones...</h4>
                                    </div>
                                    <div class="modal-body">
                                        <ol id="listModalMenu">
                                        </ol>
                                    </div>
                                    <div class="modal-footer">
                                        <a id="btnClose" href="#" class="btn btn-primary" onclick="$('#menuModal').modal('hide');">
                                            Cerrar</a>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="border: solid 1px; border-color: #FFFFFF;">
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div style="float: right; z-index: 9; width:60%; height: 75px; text-align:right; margin-right: 1em; position: absolute; right: 0;
        top: 0; margin-top: 20px;">
        <uc1:User ID="ucUserData" runat="server" />
    </div> 
    <form id="HFFrm" runat="server">
    <asp:HiddenField ID="hfUGr" runat="server" />
    </form>
    <div id="contenedor" style="float: left; z-index: 10; width: 271px; height: 75px;
        position: absolute; left: 0; top: 0;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tbody>
                <tr>
                    <td>
                    </td>
                    <td style="height: 90%">
                    </td>
                </tr>
                <tr>
                    <td>
                        <img alt="" src="/App_Themes/GattacaAdmin/Images/Template/V9/logo2.png" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</body>
</html>
