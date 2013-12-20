Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addAccumulationIndicatorSet
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
            Dim ind As String = Request.QueryString("ind")
            Dim idindicator As Integer = 0
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim facade As New Facade
            Dim objAccumulationIndicatorSet As New AccumulationIndicatorSetEntity
            Dim objIndicator As New IndicatorEntity
            Dim Item As New ListItem

            ' observar si el parametro de entrada es un numero correspondiente a un Id
            Integer.TryParse(ind, idindicator)

            ' cargar el titulo
            Session("lblTitle") = "Configurar Acumulación de Indicadores "

            Try
                ' cargar el objeto
                If (idindicator <> 0) Then
                    'cargar el registro referenciado
                    objAccumulationIndicatorSet = facade.loadAccumulationIndicatorSet(applicationCredentials, 0, idindicator:=idindicator)

                    ' cargar el indicador referenciado
                    objIndicator = facade.loadIndicator(applicationCredentials, idindicator)

                    ' si existe el registro referenciado
                    If (objAccumulationIndicatorSet.id <> 0) Then
                        ' mostrar los valores
                        Me.txtid.Text = objAccumulationIndicatorSet.id
                        Me.txtindicatorcode.Text = objAccumulationIndicatorSet.idindicator
                        Me.txtcode.Text = objAccumulationIndicatorSet.code
                        Me.txtdescription.Text = objAccumulationIndicatorSet.description
                        Me.ddlname.Items.Add(objIndicator.ENTITYNAME)
                        Me.txtiduser.Text = objAccumulationIndicatorSet.USERNAME
                        Me.txtcreatedate.Text = objAccumulationIndicatorSet.createdate
                        Me.lblid.Enabled = False
                        Me.txtid.Enabled = False
                        Me.lblcreatedate.Enabled = False
                        Me.txtcreatedate.Enabled = False
                        Me.lbliduser.Enabled = False
                        Me.txtiduser.Enabled = False
                        Me.rfvid.Enabled = False
                        Me.rfviduser.Enabled = False
                        Me.rfvcreatedate.Enabled = False

                    Else
                        Me.txtindicatorcode.Text = objIndicator.code
                        Me.ddlname.Items.Add(objIndicator.ENTITYNAME)
                        Me.lblid.Visible = False
                        Me.txtid.Visible = False
                        Me.lblcreatedate.Visible = False
                        Me.txtcreatedate.Visible = False
                        Me.lbliduser.Visible = False
                        Me.txtiduser.Visible = False
                        Me.rfvid.Enabled = False
                        Me.rfviduser.Enabled = False
                        Me.rfvcreatedate.Enabled = False

                    End If

                    ' cargar el tipo y lista doble
                    'Select Case objIndicator.levelindicator
                    '    Case "1"
                    '        loadIndicator("Segundo Nivel")
                    '        Me.ddltype.Items.Add("Primer Nivel")

                    '    Case "2.1"
                    '        loadIndicator("Tercer Nivel")
                    '        Me.ddltype.Items.Add("Segundo Nivel - Linea Estrategica")
                    '    Case "2.2"
                    '        loadIndicator("Tercer Nivel")
                    '        Me.ddltype.Items.Add("Segundo Nivel - Estrategia")
                    '    Case "3"
                    '        Me.ddltype.Items.Add("Tercer Nivel")
                    'End Select
                    Select Case objIndicator.levelindicator
                        Case "1.1"
                            loadIndicator("Primer Nivel - Linea")
                            Me.ddltype.Items.Add("Primer Nivel - Linea Estrategica ")

                        Case "1.2"
                            loadIndicator("Primer Nivel - Estra")
                            Me.ddltype.Items.Add("Primer Nivel - Estrategia")
                        Case "2"
                            loadIndicator("Segundo Nivel")
                            Me.ddltype.Items.Add("Segundo Nivel - Programa")
                        Case "3"
                            Me.ddltype.Items.Add("Tercer Nivel - Proyecto")
                    End Select

                    If Not objAccumulationIndicatorSet.INDICATORBYACCUMULATIONINDICATORSETLIST Is Nothing Then
                        For Each objIndicatorByAccumulationIndicatorSet As IndicatorByAccumulationIndicatorSetEntity In objAccumulationIndicatorSet.INDICATORBYACCUMULATIONINDICATORSETLIST
                            Item = Me.dlbIndicator.AviableItems.Items.FindByValue(objIndicatorByAccumulationIndicatorSet.idindicator)
                            Me.dlbIndicator.AviableItems.Items.Remove(Item)
                            If Not (Item Is Nothing) Then
                                Me.dlbIndicator.SelectedItems.Items.Add(Item)
                            End If
                        Next
                    End If
                Else
                    Response.Redirect("searchIndicator.aspx")

                End If

            Catch ex As Exception

                ' ir a error
                Session("sError") = ex.Message
                Session("sUrl") = Request.UrlReferrer.PathAndQuery
                Response.Redirect("~/errors/error.aspx")
                Response.End()

            Finally

                ' liberar recursos
                facade = Nothing
                objAccumulationIndicatorSet = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchIndicator.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objAccumulationIndicatorSet As New AccumulationIndicatorSetEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim id As Integer = 0
        Dim ind As String = Request.QueryString("ind")
        Dim idindicator As Integer = 0
        Dim objindicator As New IndicatorEntity
        Dim objIndicatorByAccumulationIndicatorSetList As New List(Of IndicatorByAccumulationIndicatorSetEntity)

        'Post-verificación de codigo
        If Not verifyCode() Then
            Return
        End If


        ' observar si el parametro de entrada es un numero correspondiente a un Id
        Integer.TryParse(ind, idindicator)

        ' cargar el registro referenciado
        objAccumulationIndicatorSet = facade.loadAccumulationIndicatorSet(applicationCredentials, id)
        objindicator = facade.loadIndicator(applicationCredentials, idindicator)


        If (objAccumulationIndicatorSet.id <> 0) Then
            ' cargar los valores registrados por el usuario
            objAccumulationIndicatorSet.description = Me.txtdescription.Text
            objAccumulationIndicatorSet.code = Me.txtcode.Text
            objAccumulationIndicatorSet.name = Me.ddlname.SelectedItem.Text
        Else
            ' cargar los valores registrados por el usuario
            objAccumulationIndicatorSet.idindicator = objindicator.id
            objAccumulationIndicatorSet.code = Me.txtcode.Text
            objAccumulationIndicatorSet.description = Me.txtdescription.Text
            objAccumulationIndicatorSet.name = Me.ddlname.SelectedItem.Text
            objAccumulationIndicatorSet.iduser = applicationCredentials.UserID
            objAccumulationIndicatorSet.createdate = Now
        End If

        'cargar la lista de indicadores asociados
        For Each item As ListItem In Me.dlbIndicator.SelectedItems.Items
            Dim objIndicatorByAccumulationIndicatorSet As New IndicatorByAccumulationIndicatorSetEntity
            objIndicatorByAccumulationIndicatorSet.idindicator = item.Value
            If (objAccumulationIndicatorSet.id <> 0) Then
                objIndicatorByAccumulationIndicatorSet.idaccumulationindicatorset = objAccumulationIndicatorSet.id
            End If
            objIndicatorByAccumulationIndicatorSetList.Add(objIndicatorByAccumulationIndicatorSet)
        Next
        objAccumulationIndicatorSet.INDICATORBYACCUMULATIONINDICATORSETLIST = objIndicatorByAccumulationIndicatorSetList

        Try

            ' almacenar la entidad
            If (objAccumulationIndicatorSet.id = 0) Then
                facade.addAccumulationIndicatorSet(applicationCredentials, objAccumulationIndicatorSet)
            Else
                facade.updateAccumulationIndicatorSet(applicationCredentials, objAccumulationIndicatorSet)
            End If


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
            objAccumulationIndicatorSet = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        verifyCode()
    End Sub

#End Region

#Region "Metodos"

    Private Sub loadIndicator(ByVal level As String)
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            ' cargar la lista de los tipos
            Me.dlbIndicator.AviableItems.DataSource = facade.getIndicatorList(applicationCredentials, levelname:=level)
            Me.dlbIndicator.AviableItems.DataValueField = "Id"
            Me.dlbIndicator.AviableItems.DataTextField = "code"
            Me.dlbIndicator.AviableItems.DataBind()

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

            If facade.verifyAccumulationIndicatorSetCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
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
