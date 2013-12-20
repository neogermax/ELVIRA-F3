Option Strict On

Imports Gattaca.Application.Credentials
Imports Gattaca.Application.ExceptionManager
Imports System.Xml
Imports System.Data
Imports System.Xml.Serialization
Imports System.IO
Imports System.Text
Imports System.Globalization

Public Class PublicFunction

    ' defini el nombre
    Const MODULENAME As String = "PublicFunction"

    ''' <summary>
    ''' Contruir las credenciales del usuario
    ''' </summary>
    ''' <param name="sUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function buildApplicationCredentials(ByVal clientName As String, _
                                                        ByVal request As HttpRequest, _
                                                        ByVal sUserId As Long, _
                                                        ByVal sessionId As String, _
                                                        Optional ByVal isOtherDataBase As Boolean = False) As ApplicationCredentials

        ' definir los objetos
        Dim sUserMachineIp As String
        Dim sUserSessionId As String
        Dim productName As String = "FSC"

        If isOtherDataBase Then

            ' cambiar el nombre del producto
            productName = "VBSecurity" 'PublicFunction.getSettingValue("autenticationProductName")

        End If

        ' obtener la ip del usuario
        sUserMachineIp = request.ServerVariables("HTTP_X_FORWARDED_FOR")

        ' se chequea si hay un proxy
        If sUserMachineIp = "" Then
            ' tomar la remota
            sUserMachineIp = request.ServerVariables("REMOTE_ADDR")

        End If

        ' obtener la session del usuario
        sUserSessionId = sessionId

        ' crear las nuevas credenciales
        buildApplicationCredentials = New ApplicationCredentials(clientName, productName, sUserMachineIp, sUserSessionId, userid:=sUserId)

    End Function

    ''' <summary>
    ''' Contruir las credenciales del usuario
    ''' </summary>
    ''' <param name="sUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function buildApplicationCredentials(Optional ByVal clientName As String = "", _
                                                       Optional ByVal sUserId As Long = 0, _
                                                       Optional ByVal sUserSessionId As String = "", _
                                                       Optional ByVal sUserMachineIp As String = "", _
                                                       Optional ByVal productName As String = "") As ApplicationCredentials
        ' cargar el nombre del cliente
        If clientName.Equals(String.Empty) Then

            ' cargar por defecto
            clientName = ConfigurationManager.AppSettings("clientName")

        End If

        ' cargar el usuario
        If sUserId = 0 Then

            ' cargar por defecto
            sUserId = 1

        End If

        ' obtener la ip del usuario
        If sUserMachineIp.Equals(String.Empty) Then

            ' obtener la ip del usuario
            sUserMachineIp = "127.0.0.1"

        End If

        ' obtener la session del usuario
        If sUserSessionId.Equals(String.Empty) Then

            ' obtener la session del usuario
            sUserSessionId = "1"

        End If

        ' cargar el nombre del producto
        If productName.Equals(String.Empty) Then

            ' cargar el nombre del producto
            'productName = ConfigurationManager.AppSettings("productName")
            productName = "VBSecurity"

        End If

        ' crear las nuevas credenciales
        buildApplicationCredentials = New ApplicationCredentials(clientName, productName, sUserMachineIp, sUserSessionId, userid:=sUserId)

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function convertBoolean(ByVal value As Boolean) As Integer

        If value Then
            Return 1
        Else
            Return 0
        End If

    End Function

    ''' <summary>
    ''' Obtener un valor boolean
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getBoolean(ByVal value As Object) As Boolean

        Try
            ' obtener el valor
            Return CBool(value)

        Catch ex As Exception

            'no valido
            Return False

        End Try

    End Function

    '''' <summary>
    '''' Cargar el archivo anexo
    '''' </summary>
    '''' <param name="request"></param>
    '''' <remarks></remarks>
    'Public Shared Function LoadFile(ByVal request As HttpRequest) As String

    '    ' verificar que exista el archivo
    '    If request.Files(0).FileName <> "" Then

    '        ' definiendo los objtetos
    '        Dim strFileName() As String
    '        Dim objFile As HttpPostedFile
    '        Dim fileName As String = String.Empty

    '        Try
    '            ' capturando el archivo de los parametros
    '            objFile = request.Files(0)

    '            strFileName = objFile.FileName.Split("\".ToCharArray)

    '            ' dar nombre al anexo
    '            fileName = Now.ToString("yyyyMMddhhmmss") & "_" & strFileName(strFileName.Length - 1)

    '            ' determinanado la ruta destino
    '            Dim sFullPath As String = HttpContext.Current.Server.MapPath(PublicFunction.getSettingValue("documentPath")) & "\" & fileName

    '            ' subiendo el archivo al server
    '            objFile.SaveAs(sFullPath)

    '            ' return 
    '            Return fileName

    '        Catch ex As Exception

    '            ' publicar el error
    '            GattacaApplication.Publish(ex, "", MODULENAME, "LoadFile")
    '            ExceptionPolicy.HandleException(ex, "Error. Problemas al cargar el archivo.")

    '            'lanzando el error
    '            Throw New Exception("Error. Problemas al cargar el archivo.")

    '        Finally
    '            ' liberar recursos
    '            strFileName = Nothing
    '            objFile = Nothing

    '        End Try


    '    Else
    '        ' retornar vacio
    '        Return String.Empty

    '    End If

    'End Function

    ''' <summary>
    ''' Cargar el archivo anexo
    ''' </summary>
    ''' <param name="request"></param>
    ''' <remarks></remarks>
    Public Shared Function LoadFile(ByVal request As HttpRequest) As String

        ' verificar que exista el archivo
        If request.Files(0).FileName <> "" Then

            ' definiendo los objtetos
            Dim strFileName() As String
            Dim objFile As HttpPostedFile
            Dim fileName As String = String.Empty

            Try
                ' capturando el archivo de los parametros
                objFile = request.Files(0)

                strFileName = objFile.FileName.Split("\".ToCharArray)

                ' dar nombre al anexo
                fileName = Now.ToString("yyyyMMddhhmmss") & "_" & strFileName(strFileName.Length - 1)

                ' determinanado la ruta destino
                Dim sFullPath As String = HttpContext.Current.Server.MapPath(PublicFunction.getSettingValue("documentPath")) & "\" & fileName

                ' subiendo el archivo al server
                objFile.SaveAs(sFullPath)

                ' return 
                Return fileName

            Catch ex As Exception

                ' publicar el error
                GattacaApplication.Publish(ex, "", MODULENAME, "LoadFile")
                ExceptionPolicy.HandleException(ex, "Error. Problemas al cargar el archivo.")

                'lanzando el error
                Throw New Exception("Error. Problemas al cargar el archivo.")

            Finally
                ' liberar recursos
                strFileName = Nothing
                objFile = Nothing

            End Try


        Else
            ' retornar vacio
            Return String.Empty

        End If

    End Function

    ''' <summary>
    ''' Permite eliminar un archivo previamente almacenado
    ''' </summary>
    ''' <param name="pathFile">ruta del archivo a eliminar</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteFile(ByVal pathFile As String)

        ' verificar que exista el archivo
        If (pathFile.Length > 0) Then

            ' definiendo los objtetos
            Dim miRutaArchivo As String = ""

            Try

                'Se extrae la ruta del archivo nateriormente almacenado
                miRutaArchivo = PublicFunction.getSettingValue("documentPath") & "/" & pathFile
                miRutaArchivo = HttpContext.Current.Server.MapPath(miRutaArchivo)
                Dim miFileInfo As New FileInfo(miRutaArchivo)
                'Se verifica si el archivo anterior existe
                If (miFileInfo.Exists) Then
                    'Se elimina el archivo anterior
                    File.Delete(miRutaArchivo)
                End If

            Catch ex As Exception

                ' publicar el error
                GattacaApplication.Publish(ex, "", MODULENAME, "LoadFile")
                ExceptionPolicy.HandleException(ex, "Error. Problemas al eliminar el archivo.")

                'lanzando el error
                Throw New Exception("Error. Problemas al eliimnar el archivo.")

            Finally


            End Try

        End If

    End Sub

    Public Shared Function ThemeExists(ByVal theme As String) As Boolean

        ' retornar la lista de themas existentes
        Return System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/App_Themes/" & theme))

    End Function

    ''' <summary>
    ''' Obtener el valor de un parametro
    ''' </summary>
    ''' <param name="param"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getSettingValue(ByVal param As String) As String

        ' cargar los valores desde la session
        Dim settings As List(Of Setting) = DirectCast(HttpContext.Current.Application("settings"), List(Of Setting))
        Dim value As String = ""

        If settings IsNot Nothing Then

            For Each setting In settings

                If setting.parameterName.ToUpper.Equals(param.ToUpper) Then

                    ' retornar
                    Return setting.parameterValue

                End If

            Next

        End If

        ' retornar el valor del parametro vacio
        Return value

    End Function

    ''' <summary> 
    ''' 
    ''' </summary> 
    ''' <param name="node"></param> 
    ''' <param name="nodeName"></param> 
    ''' <returns></returns> 
    Public Shared Function getNode(ByVal node As XmlNode, ByVal nodeName As String) As XmlNode
        ' verificar si es el nodo que se busca 
        If node.Name.ToUpper().Equals(nodeName.ToUpper()) Then
            ' devolverlo 
            Return node
        Else
            ' para cada uno de sus hijos 
            For Each child As XmlNode In node
                ' buscar en los hijos 
                Dim response As XmlNode = getNode(child, nodeName)

                ' si se encontro 
                If response IsNot Nothing Then
                    Return response
                End If
            Next
        End If
        Return Nothing
    End Function

    Public Shared Function StrXml2Table(ByVal sXml As String) As DataTable
        Dim ms As MemoryStream = Nothing
        Try
            Dim buf() As Byte
            Dim ds As New DataSet
            buf = System.Text.UTF8Encoding.ASCII.GetBytes(sXml)
            ms = New MemoryStream(buf)
            ds.ReadXml(ms)
            Return ds.Tables(0)
        Catch ex As Exception

            ' publicar el error
            GattacaApplication.Publish(ex, "", MODULENAME, "LoadFile")
            ExceptionPolicy.HandleException(ex, "Error. Problemas al cargar el archivo.")

            'lanzando el error
            Throw New Exception("Error. Problemas al procesar el xml.")

        Finally
            If Not ms Is Nothing Then
                ms.Close()
            End If
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EncryptData(ByVal data As String) As String

        ' definir los objetos
        Dim des As New Simple3Des

        ' cifrar
        Return des.EncryptData(data)

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DecryptData(ByVal data As String) As String

        ' definir los objetos
        Dim des As New Simple3Des

        ' cifrar
        Return des.DecryptData(data)

    End Function

    '''' <summary>
    '''' Metodo que permite convertir una cadena a un valor double sin importar la configuración regional
    '''' </summary>
    '''' <param name="number">valor en formato cadena que se desea convertir</param>
    '''' <returns>valor double convertido</returns>
    '''' <remarks></remarks>
    'Public Shared Function ConvertStringToDouble(ByVal number As String) As Double
    '    Dim miSeparadorDecimal As String = ""
    '    Dim miCantidadDecimales As Integer = 0
    '    Dim miValorDecimal As String = ""
    '    Dim miValor As Double = 0

    '    If (number.Length > 0) Then

    '        'Se consulta el separador decimal segun la configuración regional de la máquina
    '        miSeparadorDecimal = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

    '        'Se valida si el separador decimal es el punto
    '        If (miSeparadorDecimal.Equals(".") AndAlso number.Contains(miSeparadorDecimal)) Then
    '            miValor = Convert.ToDouble(number, CultureInfo.InvariantCulture)
    '        Else
    '            'Se verifica si el valor actual posee decimales
    '            If (number.IndexOf(miSeparadorDecimal) >= 0) Then
    '                'Se calcula la cantidad de decimales existentes en el vlaor actual
    '                miCantidadDecimales = (number.Length - (number.IndexOf(miSeparadorDecimal) + 1))
    '                'Se transforma el formato del valor actual a un valor con formato númerico invariant
    '                miValorDecimal = number.Substring(number.Length - miCantidadDecimales, miCantidadDecimales)
    '                number = number.Substring(0, number.Length - (miCantidadDecimales + 1)).Replace(".", ",")
    '                number = number & "." & miValorDecimal
    '                miValor = Convert.ToDouble(number, CultureInfo.InvariantCulture)
    '            Else
    '                miValor = Convert.ToDouble(number.Replace(".", ","), CultureInfo.InvariantCulture)
    '            End If
    '        End If

    '    End If

    '    'Se retorna el vlaor obtenido
    '    Return miValor

    'End Function

    ''' <summary>
    ''' Metodo que permite convertir una cadena a un valor double sin importar la configuración regional
    ''' </summary>
    ''' <param name="number">valor en formato cadena que se desea convertir</param>
    ''' <returns>valor double convertido</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertStringToDouble(ByVal number As String) As Double
        Dim miSeparador As String = ""
        Dim miValor As Double = 0

        If (number.Length > 0) Then

            'Se consulta el separador decimal segun la configuración regional de la máquina
            Dim culture As String = "en-us"
            culture = CultureInfo.CurrentCulture.Name
            Dim info As System.Globalization.CultureInfo = New System.Globalization.CultureInfo(culture)
            Dim numberInfo As System.Globalization.NumberFormatInfo = info.NumberFormat
            Dim dateInfo As System.Globalization.DateTimeFormatInfo = info.DateTimeFormat
            miValor = Decimal.Parse(number, numberInfo)

        End If

        'Se retorna el vlaor obtenido
        Return miValor

    End Function


End Class
