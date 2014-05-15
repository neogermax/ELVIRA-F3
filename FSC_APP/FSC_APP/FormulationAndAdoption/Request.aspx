<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/mpAdmin.master"
    CodeBehind="Request.aspx.vb" Inherits="FSC_APP.Request" Title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/bootstrap.min.css" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function(){
            $("input[type='text'], select, textarea").addClass("form-control");
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
        <h1>
            SOLICITUD DE PROYECTO: 118 - PROYECTO 2</h1>
        <div class="comun-controls">
            <h2>
                Contrato/Convenio/ODSC No. 000001</h2>
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
                No. de la Solicitud: 003</h2>
            <h2>
                Fecha de la solicitud: 2014/05/15 03:57 p.m.</h2>
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
                Fecha de Incio: 2014/01/01</label>
            <label>
                Fecha de Incio: 2014/01/01</label>
            <label>
                Fecha de Incio: 2014/01/01</label>
        </div>
    </div>
</asp:Content>
