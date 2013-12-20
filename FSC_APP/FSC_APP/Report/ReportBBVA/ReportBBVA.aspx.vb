Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class Report_ReportBBVA_ReportBBVA
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If HttpContext.Current.Session("Theme") IsNot Nothing Then

            ' quemar el tema del cliente
            Page.Theme = HttpContext.Current.Session("Theme").ToString

        Else
            ' quemar el tema por defecto
            Page.Theme = "GattacaAdmin"

        End If

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Page.IsPostBack Then

            'Se llama al metodo que recarga el reporte
            Me.LoadReport()


        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            LoadCombos()
        End If

        ' poner el titulo
        Session("lblTitle") = "Consulta Detalladas del Negocio"

    End Sub

  
    Protected Sub btnClick_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClick.Click
        ' cargar el reporte
        LoadReport()
    End Sub
    
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Permite poblar las diferentes listas del formulario actual
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadCombos()

        ' definir los objetos
        Dim facade As New ReportFacadeTemp
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)

        Try
            '' cargar los usuarios registrados
            Me.ddlActivity.DataSource = facade.loadReportBBVAActivity(applicationCredentials)
            Me.ddlActivity.DataValueField = "ID"
            Me.ddlActivity.DataTextField = "Name"
            Me.ddlActivity.DataBind()

            ' agregar la opcion de todos
            'Me.ddlActivity.Items.Clear()
            Me.ddlActivity.Items.Add(New ListItem("Todos", ""))
            'If (ddlLaboralSituation.SelectedValue = "Asalariado Declarante" Or ddlLaboralSituation.SelectedValue = "Asalariado No Declarante" Or ddlLaboralSituation.SelectedValue = "Pensionado Declarante" Or ddlLaboralSituation.SelectedValue = "Pensionado No Declarante") And ddlKindofClient.SelectedValue = "Cliente BBVA Cliente/No Cliente prioritario" Then
            '    Me.ddlActivity.Items.Add(New ListItem("Consulta y verificación en Bases de Datos", "1045"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Centro de formalización", "1046"))
            '    Me.ddlActivity.Items.Add(New ListItem("Realizar Maqueta A", "1047"))
            '    Me.ddlActivity.Items.Add(New ListItem("Dictamen Riesgos Preaprobado", "1048"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante Documental Preaprobado", "1051"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Credito no viable", "1049"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Crédito Pre-aprobado", "1050"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación y Validación CA", "1055"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Referenciación y Validación", "1059"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Validación Negado Segundo Intento", "1063"))
            '    Me.ddlActivity.Items.Add(New ListItem("Evidente Negado Segundo Intento", "1067"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Estudio Terminado", "1071"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante Documental Referenciación y Validación", "1075"))
            '    Me.ddlActivity.Items.Add(New ListItem("Alta y scoring CA", "1079"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante documental Alta y Scoring", "1083"))
            '    Me.ddlActivity.Items.Add(New ListItem("Dictamen Riesgos Aprobado", "1087"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Riesgos", "1091"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Dictamen de Riesgos", "1095"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de Condiciones", "1099"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de Condiciones Negación", "1103"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de condiciones Gestor", "1107"))
            '    Me.ddlActivity.Items.Add(New ListItem("Factura Proforma", "1111"))
            '    Me.ddlActivity.Items.Add(New ListItem("Elaboración Carta", "1115"))
            '    Me.ddlActivity.Items.Add(New ListItem("Elaborar Prendas CA", "1119"))
            '    Me.ddlActivity.Items.Add(New ListItem("Verificación Documental CA", "1123"))
            '    Me.ddlActivity.Items.Add(New ListItem("Firmar Cliente", "1127"))
            '    Me.ddlActivity.Items.Add(New ListItem("Apertura de Cuenta", "1131"))
            '    Me.ddlActivity.Items.Add(New ListItem("Entregar Matricula", "1135"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos al Centro de Formalización CA B", "1143"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos al Centro de Formalización CA OC", "1144"))
            '    Me.ddlActivity.Items.Add(New ListItem("Recepción de documentos CA", "1151"))
            '    Me.ddlActivity.Items.Add(New ListItem("Contabilización y Desembolso", "1155"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos a Custodio CA", "1163"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación y Validación Parcial CA", "1252"))
            '    Me.ddlActivity.Items.Add(New ListItem("Pago de Comisiones CA", "1256"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Pago de Comisiones CA", "1277"))
            '    Me.ddlActivity.Items.Add(New ListItem("Pago de comisiones Analista Banco", "1281"))
            'ElseIf (ddlLaboralSituation.SelectedValue = "Asalariado Declarante" Or ddlLaboralSituation.SelectedValue = "Asalariado No Declarante" Or ddlLaboralSituation.SelectedValue = "Pensionado Declarante" Or ddlLaboralSituation.SelectedValue = "Pensionado No Declarante") And ddlKindofClient.SelectedValue = "No cliente" Then
            '    Me.ddlActivity.Items.Add(New ListItem("Consulta y verificación en Bases de Datos", "1045"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Centro de formalización", "1046"))
            '    Me.ddlActivity.Items.Add(New ListItem("Realizar Maqueta A", "1047"))
            '    Me.ddlActivity.Items.Add(New ListItem("Dictamen Riesgos Preaprobado", "1048"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Credito no viable", "1049"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Crédito Pre-aprobado", "1050"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante Documental Preaprobado", "1051"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación Y Validación NCA", "1057"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Referenciación y Validación", "1061"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Validación Negado Segundo Intento", "1065"))
            '    Me.ddlActivity.Items.Add(New ListItem("Evidente Negado Segundo Intento", "1069"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Estudio Terminado", "1073"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante Documental Referenciación y Validación", "1077"))
            '    Me.ddlActivity.Items.Add(New ListItem("Alta y scoring NCA", "1081"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante documental Alta y Scoring", "1085"))
            '    Me.ddlActivity.Items.Add(New ListItem("Dictamen Riesgos Aprobado", "1089"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Riesgos", "1093"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Dictamen de Riesgos", "1097"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de Condiciones", "1101"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de Condiciones Negación", "1105"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de condiciones Gestor", "1109"))
            '    Me.ddlActivity.Items.Add(New ListItem("Factura Proforma", "1113"))
            '    Me.ddlActivity.Items.Add(New ListItem("Elaboración Carta", "1117"))
            '    Me.ddlActivity.Items.Add(New ListItem("Elaborar Prendas NCA", "1121"))
            '    Me.ddlActivity.Items.Add(New ListItem("Verificación Documental NCA", "1125"))
            '    Me.ddlActivity.Items.Add(New ListItem("Firmar Cliente", "1129"))
            '    Me.ddlActivity.Items.Add(New ListItem("Apertura de Cuenta", "1133"))
            '    Me.ddlActivity.Items.Add(New ListItem("Entregar Matricula", "1137"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos al Centro de Formalización NCA B", "1147"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos al Centro de Formalización NCA OC", "1148"))
            '    Me.ddlActivity.Items.Add(New ListItem("Recepción de documentos NCA", "1153"))
            '    Me.ddlActivity.Items.Add(New ListItem("Contabilización y Desembolso", "1157"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos a Custodio NCA", "1165"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación y Validación Parcial NCA", "1253"))
            '    Me.ddlActivity.Items.Add(New ListItem("Pago de Comisiones NCA", "1274"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Pago de Comisiones NCA", "1278"))
            '    Me.ddlActivity.Items.Add(New ListItem("Pago de comisiones Analista Banco", "1282"))
            'ElseIf (ddlLaboralSituation.SelectedValue = "Independiente Declarante" Or ddlLaboralSituation.SelectedValue = "Independiente No Declarante") And ddlKindofClient.SelectedValue = "Cliente BBVA Cliente/No Cliente prioritario" Then
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Centro de formalización", "1046"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Credito no viable", "1049"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Crédito Pre-aprobado", "1050"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante Documental Preaprobado", "1051"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación Y Validación", "1056"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Referenciación y Validación", "1060"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Validación Negado Segundo Intento", "1064"))
            '    Me.ddlActivity.Items.Add(New ListItem("Evidente Negado Segundo Intento", "1068"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Estudio Terminado", "1072"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante Documental Referenciación y Validación", "1076"))
            '    Me.ddlActivity.Items.Add(New ListItem("Alta y scoring CI", "1080"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante documental Alta y Scoring", "1084"))
            '    Me.ddlActivity.Items.Add(New ListItem("Dictamen Riesgos Aprobado", "1088"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Riesgos", "1092"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Dictamen de Riesgos", "1096"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de Condiciones", "1100"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de Condiciones Negación", "1104"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de condiciones Gestor", "1108"))
            '    Me.ddlActivity.Items.Add(New ListItem("Factura Proforma", "1112"))
            '    Me.ddlActivity.Items.Add(New ListItem("Elaboración Carta", "1116"))
            '    Me.ddlActivity.Items.Add(New ListItem("Elaborar Prendas CI", "1120"))
            '    Me.ddlActivity.Items.Add(New ListItem("Verificación Documental CI", "1124"))
            '    Me.ddlActivity.Items.Add(New ListItem("Firmar Cliente", "1128"))
            '    Me.ddlActivity.Items.Add(New ListItem("Apertura de Cuenta", "1132"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos al Centro de Formalización CI B", "1145"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos al Centro de Formalización CI OC", "1146"))
            '    Me.ddlActivity.Items.Add(New ListItem("Recepción de documentos CI", "1152"))
            '    Me.ddlActivity.Items.Add(New ListItem("Contabilización y Desembolso", "1156"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos a Custodio CI", "1164"))
            '    Me.ddlActivity.Items.Add(New ListItem("Consulta y verificación en Bases de Datos Independiente", "1182"))
            '    Me.ddlActivity.Items.Add(New ListItem("Realizar Maqueta Independiente", "1183"))
            '    Me.ddlActivity.Items.Add(New ListItem("Dictamen Riesgos Preaprobado I", "1186"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación y Validación CI", "1224"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación y Validación Parcial CI", "1254"))
            '    Me.ddlActivity.Items.Add(New ListItem("Pago de Comisiones CI", "1275"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Pago de Comisiones CI", "1279"))
            '    Me.ddlActivity.Items.Add(New ListItem("Pago de comisiones Analista Banco", "1283"))
            'ElseIf (ddlLaboralSituation.SelectedValue = "Independiente Declarante" Or ddlLaboralSituation.SelectedValue = "Independiente No Declarante") And ddlKindofClient.SelectedValue = "No cliente" Then
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Centro de formalización", "1046"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Credito no viable", "1049"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Crédito Pre-aprobado", "1050"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante Documental Preaprobado", "1051"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación Y Validación", "1058"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Referenciación y Validación", "1062"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Validación Negado Segundo Intento", "1066"))
            '    Me.ddlActivity.Items.Add(New ListItem("Evidente Negado Segundo Intento", "1070"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Gestor Estudio Terminado", "1074"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante Documental Referenciación y Validación", "1078"))
            '    Me.ddlActivity.Items.Add(New ListItem("Alta y scoring NCI", "1082"))
            '    Me.ddlActivity.Items.Add(New ListItem("Faltante documental Alta y Scoring", "1086"))
            '    Me.ddlActivity.Items.Add(New ListItem("Dictamen Riesgos Aprobado", "1090"))
            '    Me.ddlActivity.Items.Add(New ListItem("Devolución Riesgos", "1094"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Dictamen de Riesgos", "1098"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de Condiciones", "1102"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de Condiciones Negación", "1106"))
            '    Me.ddlActivity.Items.Add(New ListItem("Cambio de condiciones Gestor", "1110"))
            '    Me.ddlActivity.Items.Add(New ListItem("Factura Proforma", "1114"))
            '    Me.ddlActivity.Items.Add(New ListItem("Elaboración Carta", "1118"))
            '    Me.ddlActivity.Items.Add(New ListItem("Elaborar Prendas NCI", "1122"))
            '    Me.ddlActivity.Items.Add(New ListItem("Verificación Documental NCI", "1126"))
            '    Me.ddlActivity.Items.Add(New ListItem("Firmar Cliente", "1130"))
            '    Me.ddlActivity.Items.Add(New ListItem("Apertura de Cuenta", "1134"))
            '    Me.ddlActivity.Items.Add(New ListItem("Entregar Matricula", "1138"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos al Centro de Formalización NCI B", "1149"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos al Centro de Formalización NCI OC", "1150"))
            '    Me.ddlActivity.Items.Add(New ListItem("Recepción de documentos NCI", "1154"))
            '    Me.ddlActivity.Items.Add(New ListItem("Contabilización y Desembolso", "1158"))
            '    Me.ddlActivity.Items.Add(New ListItem("Enviar Documentos a Custodio NCI", "1166"))
            '    Me.ddlActivity.Items.Add(New ListItem("Consulta y verificación en Bases de Datos Independiente", "1182"))
            '    Me.ddlActivity.Items.Add(New ListItem("Realizar Maqueta Independiente", "1183"))
            '    Me.ddlActivity.Items.Add(New ListItem("Dictamen Riesgos Preaprobado I", "1186"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación y Validación NCI", "1225"))
            '    Me.ddlActivity.Items.Add(New ListItem("Referenciación y Validación Parcial NCI", "1255"))
            '    Me.ddlActivity.Items.Add(New ListItem("Pago de Comisiones NCI", "1276"))
            '    Me.ddlActivity.Items.Add(New ListItem("Notificación Pago de Comisiones NCI", "1280"))
            '    Me.ddlActivity.Items.Add(New ListItem("Pago de comisiones Analista Banco", "1284"))
            'End If
            Me.ddlActivity.SelectedValue = ""

            
        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try
    End Sub

    ''' <summary>
    ''' Permite cargar el reporte de BBVA
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadReport()

        'Declaracion de objetos
        Dim facade As New ReportFacadeTemp
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)
        Dim dt As New DataTable()
        Dim sidActivity As String = ""
        Dim sStartDateRecord As String = ""
        Dim sStartEndDateRecord As String = ""
        Dim sStartDate As String = ""
        Dim sStartEndDate As String = ""
        Dim sEndStartDate As String = ""
        Dim sEndDate As String = ""
        Dim sKindofClient As String = ""
        Dim sLaboralSituation As String = ""
        Dim sRadicado As String = ""
        Dim sNombreSolicitante As String = ""
        Dim sIdentificacion As String = ""

        Try
           
            sStartDateRecord = Request.Form("ctl00$cphPrincipal$txtStartCreationDate")
            sStartEndDateRecord = Request.Form("ctl00$cphPrincipal$txtEndCreationDate")
            sStartDate = Request.Form("ctl00$cphPrincipal$txtStartBeginDate")
            sStartEndDate = Request.Form("ctl00$cphPrincipal$txtStartEndDate")
            sEndStartDate = Request.Form("ctl00$cphPrincipal$txtEndStartDate")
            sEndDate = Request.Form("ctl00$cphPrincipal$txtEndEndDate")
            sStartEndDateRecord = Request.Form("ctl00$cphPrincipal$txtEndCreationDate")
            sidActivity = Request.Form("ctl00$cphPrincipal$ddlActivity")
            sKindofClient = Request.Form("ctl00$cphPrincipal$ddlKindofClient")
            sLaboralSituation = Request.Form("ctl00$cphPrincipal$ddlLaboralSituation")
            sRadicado = Request.Form("ctl00$cphPrincipal$txtRadicado")
            sIdentificacion = Request.Form("ctl00$cphPrincipal$txtIdentificacion")
            sNombreSolicitante = Request.Form("ctl00$cphPrincipal$txtSolicitante")

            'Se recupera el resultado de la consulta
            dt = facade.loadReportBBVA(applicationCredentials, sStartDateRecord, sStartEndDateRecord, sStartDate, sStartEndDate, sEndStartDate, sEndDate, sidActivity, sLaboralSituation, sKindofClient, sIdentificacion, sRadicado, sNombreSolicitante)

            'Se configuran los objetos
            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.Tables.Add(dt.Copy())
            ds.DataSetName = "dsRptBBVA"
            ds.Tables(0).TableName = "ReportBBVA"

            'Se carga el reporte
            rd.Load(Server.MapPath("rptBBVA.rpt"))
            rd.SetDataSource(ds)
            Me.crvReport.ReportSource = rd

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            dt = Nothing
            facade = Nothing
        End Try

    End Sub



#End Region



    
End Class
