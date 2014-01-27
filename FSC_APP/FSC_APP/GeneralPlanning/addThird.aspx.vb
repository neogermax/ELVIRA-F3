Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient

Partial Class addThird
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

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO ACTOR."

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
                    If Me.HFswchit.Value = "" Then
                        Me.HFswchit.Value = 0
                    End If

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UN ACTOR."

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
                    If Me.HFswchit.Value = "" Then
                        Me.HFswchit.Value = 0
                    End If

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objThird As New ThirdEntity


                    Try
                        ' cargar el registro referenciado
                        objThird = facade.loadThird(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores

                        Dim FirstString As String
                        Dim SecondString As String
                        Dim position As String

                        Me.txtid.Text = objThird.id

                        'TODO:34 validacion de NIT DEFAULT
                        'AUTOR:GERMAN RODRIGUEZ 28/06/2013

                        FirstString = objThird.code
                        SecondString = "NIT_D"

                        position = InStr(FirstString, SecondString)

                        If position = 1 Then
                            Me.txtcode.Text = ""
                        Else
                            Me.txtcode.Text = objThird.code
                        End If


                        Me.Txtactions.Text = objThird.documents
                        Me.Txtcontact.Text = objThird.contact
                        Me.Txtphone.Text = objThird.phone
                        Me.Txtemail.Text = objThird.email

                        Me.txtname.Text = objThird.name
                        Me.ddlenabled.SelectedValue = objThird.enabled
                        Me.txtiduser.Text = objThird.USERNAME
                        Me.txtcreatedate.Text = objThird.createdate
                        Me.Txtreplegal.Text = objThird.representantelegal
                        Me.DDLtypepeople.SelectedValue = objThird.PersonaNatural

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objThird = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        Dim nitval = Me.txtcode.Text

        If IsNumeric(nitval) Then
            Me.lblinf.Text = ""
        Else
            Me.lblinf.Text = "Sin guiones, puntos ó espacios. Incluye digito de verificación"
            Me.lblinf.ForeColor = Drawing.Color.Red
            Me.txtcode.Text = ""
            Me.txtcode.Focus()
            Exit Sub
        End If

        If Me.HFswchit.Value = 0 Then

            Me.HFswchit.Value = 1
            ' definir los objetos
            Dim facade As New Facade
            Dim objThird As New ThirdEntity
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim V_NIT As String
            'Post-verificación de código
            'If Not verifyCode() Then
            '    Return
            'End If

            Try
                ' cargar los valores registrados por el usuario
                'TODO:33 validacion de campos nuevos actores
                'AUTOR:GERMAN RODRIGUEZ 28/06/2013
                If Me.txtcode.Text = "" Then
                    Dim validatenit As String
                    V_NIT = Me.txtcode.Text
                    validatenit = validate_NIT_provicional(V_NIT)
                    If validatenit = "" Then
                        objThird.code = "NIT_D01"
                    Else
                        objThird.code = validatenit
                    End If
                Else
                    objThird.code = Me.txtcode.Text
                End If


                If Me.txtname.Text = "FSC" Or txtname.Text = "Fsc" Or txtname.Text = "Fsc" Then
                    Me.lblHelpname.Text = "El actor Fundacion Saldarriaga Concha ya existe en el sistema!"
                    Me.txtcode.Text = ""
                    Me.txtname.Text = ""
                    Me.txtname.Focus()
                    Exit Sub
                Else
                    Me.lblHelpname.Text = ""
                End If


                objThird.name = clean_vbCrLf(Me.txtname.Text)
                objThird.documents = clean_vbCrLf(Me.Txtactions.Text)
                objThird.contact = clean_vbCrLf(Me.Txtcontact.Text)
                objThird.phone = clean_vbCrLf(Me.Txtphone.Text)
                objThird.email = clean_vbCrLf(Me.Txtemail.Text)
                objThird.enabled = Me.ddlenabled.SelectedValue
                objThird.iduser = applicationCredentials.UserID
                objThird.PersonaNatural = Me.DDLtypepeople.SelectedValue
                objThird.representantelegal = clean_vbCrLf(Me.Txtreplegal.Text)
                objThird.createdate = Now

                objThird.tipodocumento = Me.DDL_tipo_doc.SelectedValue
                objThird.docrepresentante = Me.Txtdocrep.Text


                ' almacenar la entidad
                objThird.id = facade.addThird(applicationCredentials, objThird)

                ' ir al administrador

                'Me.lblexit.Text = " El Actor se creó Exitosamente "
                'Me.btnAddData.Visible = "False"
                'Me.containerSuccess.Visible = "True"
                'Me.lblexit.ForeColor = Drawing.Color.Green



            Catch oex As Threading.ThreadAbortException
                '    ' no hacer nada

            Catch ex As Exception

                ' ir a error
                Session("sError") = ex.Message
                Session("sUrl") = Request.UrlReferrer.PathAndQuery
                Response.Redirect("~/errors/error.aspx")
                Response.End()

            Finally

                ' liberar recursos
                objThird = Nothing
                facade = Nothing

            End Try
        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchThird.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objThird As New ThirdEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim V_NIT As String

        'Post-verificación de código
        'If Not verifyCode() Then
        '    Return
        'End If

        'Cargar el objeto referenciado
        objThird = facade.loadThird(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos

            'TODO:32 validacion de campos nuevos actores
            'AUTOR:GERMAN RODRIGUEZ 28/06/2013

            If Me.txtcode.Text = "" Then
                V_NIT = Me.txtcode.Text
                objThird.code = validate_NIT_provicional(V_NIT)
            Else
                objThird.code = Me.txtcode.Text
            End If

            If Me.txtname.Text = "FSC" Or txtname.Text = "Fsc" Or txtname.Text = "Fsc" Then
                Me.lblHelpname.Text = "El actor Fundacion Saldarriaga Concha ya existe en el sistema!"
                Me.txtcode.Text = ""
                Me.txtcode.Focus()
                Exit Sub
            Else
                Me.lblHelpname.Text = ""
            End If


            objThird.name = clean_vbCrLf(Me.txtname.Text)
            objThird.documents = clean_vbCrLf(Me.Txtactions.Text)
            objThird.contact = clean_vbCrLf(Me.Txtcontact.Text)
            objThird.phone = clean_vbCrLf(Me.Txtphone.Text)
            objThird.email = clean_vbCrLf(Me.Txtemail.Text)
            objThird.enabled = Me.ddlenabled.SelectedValue
            objThird.PersonaNatural = Me.DDLtypepeople.SelectedValue
            objThird.representantelegal = clean_vbCrLf(Me.Txtreplegal.Text)

            ' modificar el registro
            facade.updateThird(applicationCredentials, objThird)

            ' ir al administrador

            Me.lblexit.Text = " Actor se modificó satisfactoriamente "
            Me.containerSuccess.Visible = "True"
            Me.lblexit.ForeColor = Drawing.Color.Green
            Me.btnAddData.Visible = False
            Me.btnDelete.Visible = False

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
            objThird = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteThird(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchThird.aspx")

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

    '    Protected Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
    '        'Verificar código
    '        verifyCode()
    '    End Sub

#End Region

#Region "Metodos"

    '    Private Function verifyCode() As Boolean
    '        ' definir los objetos
    '        Dim facade As New Facade
    '        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

    '        ' obtener los parametos
    '        Dim op As String = Request.QueryString("op")

    '        Try

    '            If facade.verifyThirdCode(applicationCredentials, Me.txtcode.Text) Then
    '                Me.lblexit.Text = "El Actor ya existe, verificar la información "
    '                Me.containerSuccess.Visible = "True"
    '                Me.lblexit.ForeColor = Drawing.Color.Red
    '                Me.txtcode.Text = ""
    '                Me.txtname.Text = ""
    '                Me.Txtcontact.Text = ""
    '                Me.Txtactions.Text = ""
    '                Me.Txtphone.Text = ""
    '                Me.Txtemail.Text = ""
    '                Me.txtcode.Focus()

    '                'rfvcode.IsValid = 0
    '                verifyCode = 0
    '            Else
    '                lblexit.Text = ""
    '                Me.containerSuccess.Visible = "False"

    '                'rfvcode.IsValid = 1
    '                verifyCode = 1
    '            End If

    '        Catch oex As Threading.ThreadAbortException
    '            ' no hacer nada

    '        Catch ex As Exception

    '            ' ir a error
    '            Session("sError") = ex.Message
    '            Session("sUrl") = Request.UrlReferrer.PathAndQuery
    '            Response.Redirect("~/errors/error.aspx")
    '            Response.End()

    '        Finally

    '            ' liberar recursos
    '            facade = Nothing

    '        End Try
    '    End Function

    'TODO:31 FUNCION DE VALIDACION PARA NIT
    'AUTOR:GERMAN RODRIGUEZ 28/06/2013

    Public Function validate_NIT_provicional(ByVal nit As String) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        'consulta y validacion NIT por default
        sql.Append("exec Maxime_NIT_Default")
        Dim data = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        Dim NIT_Default As String

        NIT_Default = "NIT_D" + Convert.ToString(data)

        Return NIT_Default
    End Function

#End Region

#Region "funciones"
    ''' <summary>
    ''' TODO: 100 funcion que revisa los enter en el guardar y editar idea
    ''' autor: German Rodriguez 16/10/2013 MGgroup
    ''' </summary>
    ''' <param name="text"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function clean_vbCrLf(ByVal text As String)

        Dim pattern As String = vbCrLf
        Dim replacement As String = " "
        Dim rgx As New Regex(pattern)
        Dim result As String = rgx.Replace(text, replacement)

        Return result

    End Function
    ' TODO: 100 funcion que revisa los enter en el guardar y editar idea
    ' Autor: german Rodriguez MG group
    ' cierre de cambio

#End Region
End Class
