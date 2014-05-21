<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    ValidateRequest="false" EnableEventValidation="false" Inherits="FSC_APP.CPMain"
    Title="Panel de Control" CodeBehind="CPMain.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <link href="../css/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/elvira_F3.css" rel="stylesheet" type="text/css" />

    <script src="../Include/javascript/CPanel.js" type="text/javascript"></script>

    <script src="../js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <style>
        .ctrlpnldiv
        {
            border: 1px #888888 solid;
            -moz-box-shadow: 0px 0px 5px 0px #000000;
            -webkit-box-shadow: 0px 0px 5px 0px #000000;
            box-shadow: 0px 0px 5px 0px #000000;
            padding-top: 1em;
            padding-left: 0.5em;
            padding-bottom: 0.5em;
        }
        .ctrlfield
        {
            border: 2px solid #BFBFBF;
            padding: 1em;
            border-radius: 6px;
        }
    </style>
    <br/>
    <div>
        <asp:Label ID="lbltitulo" Style="font-size: 2em;" runat="server" Text="TITULO PROYECTO"></asp:Label>
    </div>
    <br/>
    <div>
        <fieldset class="ctrlfield">
            <legend>INICIO</legend>
            <div>
                <input id="ViewProject" class="btnCtrlPan" type="button" value="Ver proyecto" onclick="ViewProject_onclick()" />
                <input id="btnprojectapproval" class="btnCtrlPan" type="button" value="Aprobar proyecto"
                    onclick="ProjectApproval_onclick()" />
                <input id="btncontratacion" class="btnCtrlPan" type="button" value="Contratación"
                    onclick="Contract_onclick()" />
                <input id="btnproceed" class="btnCtrlPan" type="button" value="Acta de inicio" onclick="Proceed_onclick()" />
            </div>
        </fieldset>
    </div>
    <br/>
    <div>
        <fieldset class="ctrlfield">
            <legend>EJECUCION</legend>
            <div>
                <input id="btncrono" class="btnCtrlPan" type="button" value="Cronograma actividades"
                    onclick="Crono_onclick()" />
                <input id="btnriskmanagement" class="btnCtrlPan" type="button" value="Gestión de riesgos"
                    onclick="RiskManagement_onclick()" />
                <input id="btnreports" class="btnCtrlPan" type="button" value="Reportes" onclick="Reports_onclick()" />
                <input id="btnindicators" class="btnCtrlPan" type="button" value="Indicadores" onclick="Indicators_onclick()" />
                <input id="btntracing" class="btnCtrlPan" type="button" value="Seguimiento" onclick="Tracing_onclick()" />
            </div>
        </fieldset>
    </div>
    <br/>
    <div>
        <fieldset class="ctrlfield">
            <legend>MODIFICACION</legend>
            <div>
                <input id="btnmodification" class="btnCtrlPan" type="button" value="Solicitar modificación"
                    onclick="Modification_onclick()" />
                <input id="btnapproval" class="btnCtrlPan" type="button" value="Aprobar solicitud"
                    onclick="Approval_onclick()" />
                <input id="btneditcont" class="btnCtrlPan" type="button" value="Contratación modificación"
                    onclick="EditCont_onclick()" />
                    
            </div>
        </fieldset>
    </div>
    <br/>
    <div>
        <fieldset class="ctrlfield">
            <legend>CIERRE</legend>
            <div>
                <input id="btnproceedclose" class="btnCtrlPan" type="button" value="Acta cierre"
                    onclick="ProceedClose_onclick()" />
                <input id="btnlearnedlessons" class="btnCtrlPan" type="button" value="Lecciones aprendidas"
                    onclick="LearnedLessons_onclick()" />
            </div>
        </fieldset>
    </div>
    <br/>
</asp:Content>
