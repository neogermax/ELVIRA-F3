Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addIndicator
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
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            'Cargar combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO INDICADOR."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Visible = False
                    Me.txtid.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False
                    Me.rfvid.Enabled = False
                    Me.rfviduser.Enabled = False
                    Me.rfvcreatedate.Enabled = False

                    ' crear la nueva lista de fechas
                    Session("dateList") = New List(Of MeasurementDateByIndicatorEntity)

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UN INDICADOR."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Enabled = False
                    Me.txtid.Enabled = False
                    Me.lblcreatedate.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objIndicator As New IndicatorEntity

                    Try
                        ' cargar el registro referenciado
                        objIndicator = facade.loadIndicator(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objIndicator.id
                        Me.ddllevel.SelectedValue = objIndicator.levelindicator
                        ddllevel_SelectedIndexChanged(sender, e)
                        Me.ddlentity.SelectedValue = objIndicator.identity
                        Me.txtcode.Text = objIndicator.code
                        Me.txtdescription.Text = objIndicator.description
                        Me.ddltype.SelectedValue = CInt(objIndicator.type)
                        Me.txtgoal.Text = objIndicator.goal
                        Me.txtgreenvalue.Text = objIndicator.greenvalue
                        Me.txtyellowvalue.Text = objIndicator.yellowvalue
                        Me.txtredvalue.Text = objIndicator.redvalue
                        Me.txtassumptions.Text = objIndicator.assumptions
                        Me.txtsourceverification.Text = objIndicator.sourceverification
                        Me.ddlenabled.SelectedValue = objIndicator.enabled
                        Me.txtiduser.Text = objIndicator.USERNAME
                        Me.txtcreatedate.Text = objIndicator.createdate
                        Me.ddlidresponsible.SelectedValue = objIndicator.idresponsable
                        Session("dateList") = objIndicator.dateList
                        Me.gvDate.DataSource = objIndicator.dateList
                        Me.gvDate.DataBind()
                        Me.showDate.Update()

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objIndicator = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
		Dim facade As New Facade
        Dim objIndicator As New IndicatorEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de codigo
        If Not verifyCode() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objIndicator.levelindicator = ddllevel.SelectedValue
            objIndicator.identity = ddlentity.SelectedValue
            objIndicator.code = Me.txtcode.Text
            objIndicator.description = Me.txtdescription.Text
            objIndicator.type = Me.ddltype.SelectedValue
            objIndicator.goal = IIf(Me.txtgoal.Text <> "", Me.txtgoal.Text, 0)
            objIndicator.greenvalue = IIf(Me.txtgreenvalue.Text <> "", Me.txtgreenvalue.Text, 0)
            objIndicator.yellowvalue = IIf(Me.txtyellowvalue.Text <> "", Me.txtyellowvalue.Text, 0)
            objIndicator.redvalue = IIf(Me.txtredvalue.Text <> "", Me.txtredvalue.Text, 0)
            objIndicator.assumptions = Me.txtassumptions.Text
            objIndicator.sourceverification = Me.txtsourceverification.Text
            objIndicator.enabled = Me.ddlenabled.SelectedValue
            objIndicator.iduser = applicationCredentials.UserID
            objIndicator.createdate = Now
            objIndicator.idresponsable = ddlidresponsible.SelectedValue

            ' cargar las fechas de medicion
            objIndicator.dateList = Session("dateList")

            ' almacenar la entidad
            objIndicator.id = facade.addIndicator(applicationCredentials, objIndicator)

            ' ir al administrador
            Response.Redirect("searchIndicator.aspx")

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            objIndicator = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchIndicator.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
		Dim objIndicator As New IndicatorEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de codigo
        If Not verifyCode() Then
            Return
        End If

        'Cargar el objeto referenciado
        objIndicator = facade.loadIndicator(applicationCredentials, Request.QueryString("id"))

        Try
            ' cargar los datos
            objIndicator.levelindicator = ddllevel.SelectedValue
            objIndicator.identity = ddlentity.SelectedValue
            objIndicator.code = Me.txtcode.Text
            objIndicator.description = Me.txtdescription.Text
            objIndicator.type = Me.ddltype.SelectedValue
            objIndicator.goal = IIf(Me.txtgoal.Text <> "", Me.txtgoal.Text, 0)
            objIndicator.greenvalue = IIf(Me.txtgreenvalue.Text <> "", Me.txtgreenvalue.Text, 0)
            objIndicator.yellowvalue = IIf(Me.txtyellowvalue.Text <> "", Me.txtyellowvalue.Text, 0)
            objIndicator.redvalue = IIf(Me.txtredvalue.Text <> "", Me.txtredvalue.Text, 0)
            objIndicator.assumptions = Me.txtassumptions.Text
            objIndicator.sourceverification = Me.txtsourceverification.Text
            objIndicator.enabled = Me.ddlenabled.SelectedValue
            objIndicator.dateList = Session("dateList")
            objIndicator.idresponsable = ddlidresponsible.SelectedValue

            ' modificar el registro
            facade.updateIndicator(applicationCredentials, objIndicator)

            ' ir al administrador
            Response.Redirect("searchIndicator.aspx")

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
			facade = Nothing
            objIndicator = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteIndicator(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchIndicator.aspx")

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub
    
    Protected Sub btnCancelDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelDelete.Click

        ' ocultar algunos botones
        Me.btnAddData.Visible = False
        Me.btnSave.Visible = True
        Me.btnDelete.Visible = True
        Me.btnCancelDelete.Visible = False
        Me.btnConfirmDelete.Visible = False
        Me.lblDelete.Visible = False
        Me.btnCancel.Visible = True

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        ' ocultar algunos botones
        Me.btnSave.Visible = False
        Me.btnDelete.Visible = False
        Me.btnConfirmDelete.Visible = True
        Me.btnCancel.Visible = False
        Me.btnCancelDelete.Visible = True
        Me.lblDelete.Visible = True

    End Sub

    Protected Sub ddllevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddllevel.SelectedIndexChanged
        'Ajuste de los combos de entidades
        loadCombos()
    End Sub

    Protected Sub btnAddDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDate.Click

        ' cargar la lista de fechas
        Dim dateList As List(Of MeasurementDateByIndicatorEntity) = Session("dateList")
        Dim MeasurementDate As New MeasurementDateByIndicatorEntity
        
        ' guardar los datos de la fecha
        MeasurementDate.measurementdate = Me.txtDate.Text

        ' guarda los datos de la medición 
        MeasurementDate.measure = Me.txtMeasure.Text

        ' guardar los datos del tipo de medición
        MeasurementDate.measuretype = Me.dpMeasureType.SelectedValue

        ' agregar la fecha a la lista
        dateList.Add(MeasurementDate)

        ' mostrar los datos en la grilla
        Me.gvDate.DataSource = dateList
        Me.gvDate.DataBind()
        Me.upDate.Update()
        Me.showDate.Update()

        'Se limpia el control de fecha
        Me.txtDate.Text = ""
        Me.txtMeasure.Text = ""


    End Sub

    Protected Sub gvDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDate.SelectedIndexChanged

        ' cargar la lista de fechas
        Dim dateList As List(Of MeasurementDateByIndicatorEntity) = Session("dateList")


        ' agregar la fecha a la lista
        dateList.RemoveAt(Me.gvDate.SelectedIndex)

        ' mostrar los datos en la grilla
        Me.gvDate.DataSource = dateList
        Me.gvDate.DataBind()
        Me.showDate.Update()

    End Sub

    Protected Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        'Verificar código
        verifyCode()
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        Try
            Select Case Me.ddllevel.SelectedValue
                Case "2"
                    If (Request.QueryString("op").Equals("add")) Then
                        Me.ddlentity.DataSource = facade.getProgramList(applicationCredentials, enabled:="1", order:="code")
                    Else
                        Me.ddlentity.DataSource = facade.getProgramList(applicationCredentials, order:="code")
                    End If

                    Me.ddlentity.DataValueField = "Id"
                    Me.ddlentity.DataTextField = "Code"
                Case "1.1"
                    If (Request.QueryString("op").Equals("add")) Then
                        Me.ddlentity.DataSource = facade.getStrategicLineList(applicationCredentials, enabled:="1", order:="code")
                    Else
                        Me.ddlentity.DataSource = facade.getStrategicLineList(applicationCredentials, order:="code")
                    End If

                    Me.ddlentity.DataValueField = "Id"
                    Me.ddlentity.DataTextField = "Code"
                Case "1.2"
                    If (Request.QueryString("op").Equals("add")) Then
                        Me.ddlentity.DataSource = facade.getStrategyList(applicationCredentials, enabled:="1", order:="code")
                    Else
                        Me.ddlentity.DataSource = facade.getStrategyList(applicationCredentials, order:="code")
                    End If

                    Me.ddlentity.DataValueField = "Id"
                    Me.ddlentity.DataTextField = "Code"
                Case "3"
                    If (Request.QueryString("op").Equals("add")) Then
                        Me.ddlentity.DataSource = facade.getProjectList(applicationCredentials, enabled:="1", order:="code", fromIndicador:=1)
                    Else
                        Me.ddlentity.DataSource = facade.getProjectList(applicationCredentials, order:="code", fromIndicador:=1)
                    End If
                    Me.ddlentity.DataValueField = "idkey"
                    Me.ddlentity.DataTextField = "Code"

            End Select
            'Se selecciona la pestaña inicial
            Me.TabContainer1.ActiveTabIndex = 0

            ' responsable del indicador
            Me.ddlidresponsible.DataSource = facade.getUserList(applicationCredentials)
            Me.ddlidresponsible.DataValueField = "Id"
            Me.ddlidresponsible.DataTextField = "Code"
            Me.ddlidresponsible.DataBind()

            ' cargar la lista de los tipos


            Me.ddlentity.DataBind()

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

    Private Function verifyCode() As Boolean
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener los parametos
        Dim op As String = Request.QueryString("op")

        Try

            If facade.verifyIndicatorCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
                lblHelpcode.Text = "Este código ya existe, por favor cambielo"
                rfvcode.IsValid = 0
                verifyCode = 0
            Else
                lblHelpcode.Text = ""
                rfvcode.IsValid = 1
                verifyCode = 1
            End If

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try
    End Function

#End Region

End Class
