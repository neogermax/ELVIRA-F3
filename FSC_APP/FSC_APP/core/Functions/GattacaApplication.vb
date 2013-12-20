Option Strict On

Imports System.Data
Imports Gattaca.Application.ExceptionManager
Imports Gattaca.Application.Credentials
Imports System.Xml
Imports Microsoft.Win32
Imports System.Data.SqlClient
Imports FSC_APP.localhost1

Public Class GattacaApplication

    ' defini el nombre
    Const MODULENAME As String = "GattacaApplication"

    ''' <summary>
    ''' Contructor vacio de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
    End Sub

#Region "Base de datos"

    ''' <summary>
    ''' Ejecuta una Instruccion SQL sobre otra base de datos de otro producto.
    ''' Modificación MG Group Ltda.
    ''' Autor: Juan Camilo Martinez Morales
    ''' Se agrega parametro que define el tipo de ejecución a la bd
    ''' </summary>
    ''' <param name="objAppCredentials"></param>
    ''' <param name="strSP"></param>
    ''' <param name="lOperationID"></param>
    ''' <param name="objParams"></param>
    ''' <param name="CommandType"></param>
    ''' <param name="sDatabase"></param>
    ''' <param name="sProductName "></param>
    ''' <remarks></remarks>
    Shared Function RunSQL(ByVal objAppCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal strSP As String, _
                                    Optional ByVal lOperationID As Integer = 174, _
                                    Optional ByVal objParams() As System.Data.Common.DbParameter = Nothing, _
                                    Optional ByVal CommandType As System.Data.CommandType = CommandType.Text, _
                                    Optional ByVal sDatabase As String = "DB1", _
                                    Optional ByVal sProductName As String = "FSC", _
                                    Optional ByVal ScalarValue As Boolean = False) As Long


        ' definir los objetos
        Dim objAppDB As Gattaca.Application.xApplication.xApplication = Nothing
        Dim objCred As New Gattaca.Application.Credentials.ApplicationCredentials(objAppCredentials.ClientName, objAppCredentials.ProductName, _
                                                objAppCredentials.UserMachineIP, objAppCredentials.UserSessionID, objAppCredentials.UserID, _
                                                objAppCredentials.DbTypeCore)
        Try
            ' es security?
            If sProductName <> "" Then
                ' nombre del Producto que llegue por parametros
                objCred.ProductName = sProductName

            Else
                ' nombre del credentials que llegue
                objCred.ProductName = objAppCredentials.ProductName

            End If

            ' asingar el usuario
            objCred.UserID = objAppCredentials.UserID

            ' crear el objeto
            objAppDB = New Gattaca.Application.xApplication.xApplication

            ' ejecutar
            'TODO: JC Se agrega el condicional para identificar el tipo de retorno en caso de ser ejecucion o retorno de valores
            'Autor: Juan Camilo Martinez Morales.
            'Fecha: 07/Junio/2013
            If (Not ScalarValue) Then
                Return objAppDB.ExecuteNonQuery(objCred, strSP, lOperationID, objParams, CommandType, sDatabase)
            Else

                If Not IsDBNull(objAppDB.GetScalar(objAppCredentials, strSP, lOperationID, Nothing, CommandType, sDatabase)) Then
                    Return Convert.ToInt64(objAppDB.GetScalar(objAppCredentials, strSP, lOperationID, Nothing, CommandType, sDatabase))
                Else
                    Return 0
                End If
            End If


        Catch oEx As Exception

            ' publicar el error
            GattacaApplication.Publish(oEx, objAppCredentials.ClientName, MODULENAME, "RunSQL")
            ExceptionPolicy.HandleException(oEx, "GattacaStandardExceptionPolicy")

            ' lanzar la exception
            Throw oEx

        Finally

            ' verificar que este limpio
            If Not (objAppDB Is Nothing) Then

                ' liberar
                objAppDB.Dispose()

                ' desreferenciar
                objAppDB = Nothing

            End If
            ' desreferenciar
            objCred = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Retorna un dataTable.
    ''' </summary>
    ''' <param name="objAppCredentials"></param>
    ''' <param name="strSP"></param>
    ''' <param name="lOperationID"></param>
    ''' <param name="objParams"></param>
    ''' <param name="CommandType"></param>
    ''' <param name="sDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function RunSQLRDT(ByVal objAppCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal strSP As String, _
                                    Optional ByVal sProductName As String = "FSC", _
                                    Optional ByVal lOperationID As Integer = 174, _
                                    Optional ByVal objParams() As System.Data.Common.DbParameter = Nothing, _
                                    Optional ByVal CommandType As System.Data.CommandType = CommandType.Text, _
                                    Optional ByVal sDatabase As String = "DB1") As DataTable

        ' definir los objetos
        Dim objAppDB As Gattaca.Application.xApplication.xApplication = Nothing
        Dim objCred As New Gattaca.Application.Credentials.ApplicationCredentials(objAppCredentials.ClientName, objAppCredentials.ProductName, objAppCredentials.UserMachineIP, objAppCredentials.UserSessionID, objAppCredentials.UserID, objAppCredentials.DbTypeCore)

        Try
            ' cargar el nombre
            objCred.ProductName = sProductName

            ' cargar el usuario
            objCred.UserID = objAppCredentials.UserID

            ' crear el objeto
            objAppDB = New Gattaca.Application.xApplication.xApplication

            ' ejecutar
            Return objAppDB.GetDataTable(objCred, strSP, lOperationID, objParams, CommandType, sDatabase)

        Catch oEx As Exception

            ' publicar el error
            GattacaApplication.Publish(oEx, objAppCredentials.ClientName, MODULENAME, "RunSQLRDT")
            ExceptionPolicy.HandleException(oEx, "GattacaStandardExceptionPolicy")

            ' lanzar la exception
            Throw oEx

        Finally

            ' verificar que este limpio
            If Not (objAppDB Is Nothing) Then

                ' liberar
                objAppDB.Dispose()

                ' desreferenciar
                objAppDB = Nothing

            End If
            ' desreferenciar
            objCred = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Retorna un dataTable.
    ''' </summary>
    ''' <param name="objAppCredentials"></param>
    ''' <param name="strSP"></param>
    ''' <param name="lOperationID"></param>
    ''' <param name="objParams"></param>
    ''' <param name="CommandType"></param>
    ''' <param name="sDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function RunSQLRDS(ByVal objAppCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal strSP As String, _
                                    Optional ByVal sProductName As String = "FSC", _
                                    Optional ByVal lOperationID As Integer = 174, _
                                    Optional ByVal objParams() As System.Data.Common.DbParameter = Nothing, _
                                    Optional ByVal CommandType As System.Data.CommandType = CommandType.Text, _
                                    Optional ByVal sDatabase As String = "DB1") As DataSet

        ' definir los objetos
        Dim objAppDB As Gattaca.Application.xApplication.xApplication = Nothing
        Dim objCred As New Gattaca.Application.Credentials.ApplicationCredentials(objAppCredentials.ClientName, objAppCredentials.ProductName, objAppCredentials.UserMachineIP, objAppCredentials.UserSessionID, objAppCredentials.UserID, objAppCredentials.DbTypeCore)

        Try
            ' dar el nombre
            objCred.ProductName = sProductName

            ' id del usuario
            objCred.UserID = objAppCredentials.UserID

            ' crear
            objAppDB = New Gattaca.Application.xApplication.xApplication

            ' ejecutar
            Return objAppDB.GetDataSet(objCred, strSP, lOperationID)

        Catch oEx As Exception

            ' publicar el error
            GattacaApplication.Publish(oEx, objAppCredentials.ClientName, MODULENAME, "RunSQLRDS")
            ExceptionPolicy.HandleException(oEx, "GattacaStandardExceptionPolicy")

            ' lanzar la exception
            Throw oEx

        Finally

            ' verificar que este limpio
            If Not (objAppDB Is Nothing) Then

                ' liberar
                objAppDB.Dispose()

                ' desreferenciar
                objAppDB = Nothing

            End If
            ' desreferenciar
            objCred = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener el nombre registrada de la base de datos de un producto
    ''' </summary>
    ''' <param name="applicationCredentials"></param>
    ''' <param name="ProductName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDBName(ByVal applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                        Optional ByVal ProductName As String = "VBSecurity") As String

        ' verificar las credenciales del usuario
        If applicationCredentials Is Nothing Then

            ' mostrar error
            Throw New ArgumentNullException("ApplicationCredentials", "No hay credenciales definidas")

        End If

        ' definir la ruta absoluta en el registro
        Dim absolutePath As String = "SOFTWARE\VBusiness\" & ProductName & "\" + applicationCredentials.ClientName + "\DB1"

        ' cargar la informacion
        Dim key As Microsoft.Win32.RegistryKey = GetRegistryInfo(ProductName, applicationCredentials.ClientName, "\DB1")

        ' obtener el valor
        Dim result As Object = key.GetValue("DB")

        ' veificar que exista
        If result Is Nothing Then

            ' mostrar error
            Throw New ArgumentException("Las llaves solicitadas no existen para DB", absolutePath)

        End If

        ' retornar el resultado
        Return result.ToString

    End Function

    ''' <summary>
    ''' Obtiene la llave del registro de windows para un producto y cliente dados.
    ''' </summary>
    ''' <param name="productName">Producto.</param>
    ''' <param name="clientName">Cliente.</param>
    ''' <param name="relativePath">Opcional. Ruta específica adicional.</param>
    ''' <returns>LLave de registro encontrada.</returns>
    ''' <remarks></remarks>
    Public Shared Function GetRegistryInfo(ByVal productName As String, Optional ByVal clientName As String = "", Optional ByVal relativePath As String = "\") As RegistryKey
        Dim absolutePath As String = "SOFTWARE\VBusiness\" + productName + "\" + clientName + relativePath

        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey(absolutePath)

        If key Is Nothing Then
            Throw New ArgumentException("Las llaves de la licencia asignada no estan correctamente registradas", absolutePath)
        End If

        Return key
    End Function

#End Region

#Region "Funciones"

    ''' <summary>
    ''' retorna el script que genera el efecto de bloqueo y espera en la pantalla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getWaitImageScript() As String

        ' definir los objetos
        Dim script As New StringBuilder

        ' concatenar
        script.AppendLine(" 	<script> ")
        script.AppendLine("     var obj_MODAL_DIV_AJAX = document.getElementById('MODAL_DIV_AJAX'); ")
        script.AppendLine("     var obj_MODAL_DIV_AJAX_TOTAL = document.getElementById('MODAL_DIV_AJAX_TOTAL'); ")
        script.AppendLine("     Sys.UI.DomEvent.addHandler(window, 'scroll', scrollEvent); ")
        script.AppendLine("     Sys.UI.DomEvent.addHandler(window, 'resize', resizeEvent); ")
        script.AppendLine("     function scrollEvent() ")
        script.AppendLine("     {  ")
        script.AppendLine("     	mover_MODAL_DIV_AJAX();  ")
        script.AppendLine("     } ")
        script.AppendLine("     function resizeEvent() ")
        script.AppendLine("     {  ")
        script.AppendLine("     	mover_MODAL_DIV_AJAX();  ")
        script.AppendLine("     } ")
        script.AppendLine("     function mover_MODAL_DIV_AJAX() ")
        script.AppendLine("     { ")
        script.AppendLine("         var divWidth = parseInt(obj_MODAL_DIV_AJAX.style.width); ")
        script.AppendLine("         var divHeight = parseInt(obj_MODAL_DIV_AJAX.style.height); ")
        script.AppendLine("         var scrollLeft = parseInt(document.documentElement.scrollLeft); ")
        script.AppendLine("         var scrollTop = parseInt(document.documentElement.scrollTop); ")
        script.AppendLine("         var clientWidth = parseInt(document.documentElement.clientWidth); ")
        script.AppendLine("         var clientHeight = parseInt(document.documentElement.clientHeight); ")
        script.AppendLine("         var styleLeft = parseInt((clientWidth + scrollLeft) - (clientWidth / 2) - (divWidth / 2)); ")
        script.AppendLine("         var styleHeight = parseInt((clientHeight + scrollTop) - (clientHeight / 2) - (divHeight / 2)); ")
        script.AppendLine("         obj_MODAL_DIV_AJAX.style.left = parseInt(styleLeft).toString() + 'px'; ")
        script.AppendLine("         obj_MODAL_DIV_AJAX.style.top = parseInt(styleHeight).toString() + 'px'; ")
        script.AppendLine("         scrollLeft = parseInt(document.documentElement.scrollLeft); ")
        script.AppendLine("         scrollTop = parseInt(document.documentElement.scrollTop); ")
        script.AppendLine("         clientWidth = parseInt(document.documentElement.clientWidth); ")
        script.AppendLine("         clientHeight = parseInt(document.documentElement.clientHeight); ")
        script.AppendLine("         obj_MODAL_DIV_AJAX_TOTAL.style.left = parseInt(scrollLeft).toString() + 'px'; ")
        script.AppendLine("         obj_MODAL_DIV_AJAX_TOTAL.style.top = parseInt(scrollTop).toString() + 'px'; ")
        script.AppendLine("         obj_MODAL_DIV_AJAX_TOTAL.style.width = parseInt(clientWidth).toString() + 'px'; ")
        script.AppendLine("         obj_MODAL_DIV_AJAX_TOTAL.style.height = parseInt(clientHeight).toString() + 'px'; ")
        script.AppendLine("     } ")
        script.AppendLine("     /* LLAMA LA FUNCIÓN POR PRIMERA VEZ CUANDO LA PÁGINA CARGA */ ")
        script.AppendLine("     mover_MODAL_DIV_AJAX(); ")
        script.AppendLine(" 	</script> ")

        ' retornar
        Return script.ToString()

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="oEx"></param>
    ''' <param name="ClientName"></param>
    ''' <param name="ModuleName"></param>
    ''' <param name="MethodName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function Publish(ByVal oEx As Exception, _
                            ByVal ClientName As String, _
                            ByVal ModuleName As String, _
                            ByVal MethodName As String) As Exception
        ' limpiar
        oEx.Data.Clear()

        ' agregar la informacion adicional
        oEx.Data.Add("ClientName", ClientName)
        oEx.Data.Add("Module", ModuleName)
        oEx.Data.Add("Method", MethodName)

        ' retornar
        Return oEx

    End Function

#End Region

#Region "SqlDirect"


    'Shared Sub RunSQL(ByVal objAppCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '                                ByVal strSP As String)

    '    EjecutarQuerySinResultado(objAppCredentials, strSP)

    'End Sub

    'Shared Function RunSQLRDT(ByVal objAppCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '                                ByVal strSP As String, _
    '                                Optional ByVal sdatabase As String = "") As DataTable

    '    RunSQLRDT = EjecutarQuery(objAppCredentials, strSP)

    'End Function


    '''' <summary>
    '''' ejecutar un query contra la base de datos
    '''' </summary>
    '''' <param name="objAppCredentials"></param>
    '''' <param name="Query"></param>
    '''' <remarks></remarks>
    'Shared Sub EjecutarQuerySinResultado(ByVal objAppCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '                              ByVal Query As String)

    '    'Obtenemos el String de conexión directamente de un archivo de configuración
    '    Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("connectionString")
    '    Dim objConnection As SqlConnection
    '    Dim objCommand As SqlCommand

    '    Try
    '        ' abrir la conexion
    '        objConnection = New SqlConnection(ConString)
    '        objConnection.Open()

    '        ' crear el comendo
    '        objCommand = New SqlCommand(Query, objConnection)

    '        ' ejecutar el query
    '        objCommand.ExecuteNonQuery()

    '    Catch oEx As Exception

    '        ' publicar el error
    '        GattacaApplication.Publish(oEx, objAppCredentials.ClientName, MODULENAME, "EjecutarQuerySinResultado")
    '        ExceptionPolicy.HandleException(oEx, "GattacaStandardExceptionPolicy")

    '        ' lanzar la exception
    '        Throw oEx

    '    Finally

    '        ' liberar recursos
    '        objConnection.Close()
    '        objCommand = Nothing
    '        objConnection = Nothing

    '    End Try

    'End Sub

    '''' <summary>
    '''' ejecutar eun query contra la base de datos
    '''' </summary>
    '''' <param name="objAppCredentials"></param>
    '''' <param name="Query"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Shared Function EjecutarQuery(ByVal objAppCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '                       ByVal Query As String) As DataTable

    '    ' definir los obejtos
    '    Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("connectionString")
    '    Dim objDataSet As New DataSet
    '    Dim objConnection As SqlConnection
    '    'Dim objCommand As w SqlCommand

    '    Try

    '        objConnection = New SqlConnection(ConString)
    '        objConnection.Open()

    '        Dim objCommand As New SqlCommand(Query, objConnection), _
    '            objDataAdapter As New SqlDataAdapter(objCommand)



    '        objDataAdapter.Fill(objDataSet)
    '        objDataAdapter = Nothing


    '        objCommand = Nothing

    '    Catch oEx As Exception

    '        ' publicar el error
    '        GattacaApplication.Publish(oEx, objAppCredentials.ClientName, MODULENAME, "EjecutarQuery")
    '        ExceptionPolicy.HandleException(oEx, "GattacaStandardExceptionPolicy")

    '        ' lanzar la exception
    '        Throw oEx

    '    Finally

    '        objConnection.Close()
    '        objConnection = Nothing

    '    End Try

    '    ' devolcer el valor
    '    Return objDataSet.Tables(0)

    'End Function


#End Region

#Region "Metodos de conexion con el BPM"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="applicationCredentials"></param>
    ''' <param name="IdProcessCase"></param>
    ''' <param name="EntryDataType"></param>
    ''' <param name="EntryData"></param>
    ''' <param name="IDEntryData"></param>
    ''' <param name="secuence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function createProcessInstance(ByVal applicationCredentials As ApplicationCredentials, _
                                            ByVal IdProcessCase As String, _
                                            ByVal EntryDataType As String, _
                                            ByVal EntryData As String, _
                                            ByVal IDEntryData As String, _
                                            ByVal secuence As String) As Long

        ' definir los obj
        Dim bpm As New GattacaBPMServices9000
        Dim idProcessInstance As Long

        ' actualizar la url del servicio
        bpm.Url = PublicFunction.getSettingValue("BPMServices.bpmservices")

        ' crear el instancia del proceso
        idProcessInstance = bpm.WMCreateProcessInstance(applicationCredentials.ClientName, applicationCredentials.UserID, _
                                            CLng(IdProcessCase), CLng(secuence), EntryDataType, IDEntryData.ToString, EntryData)

        ' retornar
        Return idProcessInstance

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="applicationCredentials"></param>
    ''' <param name="idProcessInstance"></param>
    ''' <param name="IdProcessCase"></param>
    ''' <param name="EntryDataType"></param>
    ''' <param name="EntryData"></param>
    ''' <param name="IDEntryData"></param>
    ''' <param name="secuence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function startProcessInstance(ByVal applicationCredentials As ApplicationCredentials, _
                                            ByVal idProcessInstance As Long, _
                                            ByVal IdProcessCase As String, _
                                            ByVal EntryDataType As String, _
                                            ByVal EntryData As String, _
                                            ByVal IDEntryData As String, _
                                            ByVal secuence As String) As Long
        ' definir los objetos
        Dim bpm As New GattacaBPMServices9000
        Dim idActivcityInstance As Long

        ' actualizar la url del servicio
        bpm.Url = PublicFunction.getSettingValue("BPMServices.bpmservices")


        ' iniciar el proceso
        idActivcityInstance = bpm.WMStartProcessInstance(applicationCredentials.ClientName, applicationCredentials.UserID, _
                                            idProcessInstance)

        ' retornar
        Return idActivcityInstance

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="applicationCredentials"></param>
    ''' <param name="idProcessInstance"></param>
    ''' <param name="IdActivityInstance"></param>
    ''' <param name="Condition"></param>
    ''' <param name="comments"></param>
    ''' <param name="outComments"></param>
    ''' <param name="EntryDataType"></param>
    ''' <param name="EntryData"></param>
    ''' <param name="IDEntryData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function endActivityInstance(ByVal applicationCredentials As ApplicationCredentials, _
                                            ByVal idProcessInstance As Long, _
                                            ByVal IdActivityInstance As String, _
                                            ByVal Condition As String, _
                                            ByVal comments As String, _
                                            ByVal outComments As String, _
                                            ByVal EntryDataType As String, _
                                            ByVal EntryData As String, _
                                            ByVal IDEntryData As String) As String
        ' definir los objetos
        Dim bpm As New GattacaBPMServices9000
        Dim result As String

        ' actualizar la url del servicio
        bpm.Url = PublicFunction.getSettingValue("BPMServices.bpmservices")

        ' iniciar el proceso
        result = bpm.EndActivityInstance(applicationCredentials.ClientName, applicationCredentials.UserID, _
                                            CLng(IdActivityInstance), idProcessInstance, Condition, comments, outComments, EntryDataType, EntryData, IDEntryData)

        ' retornar
        Return result

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="applicationCredentials"></param>
    ''' <param name="idProcessInstance"></param>
    ''' <param name="IdActivityInstance"></param>
    ''' <param name="EntryDataType"></param>
    ''' <param name="EntryData"></param>
    ''' <param name="IDEntryData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function attachDataToActivityInstance(ByVal applicationCredentials As ApplicationCredentials, _
                                            ByVal idProcessInstance As Long, _
                                            ByVal IdActivityInstance As String, _
                                            ByVal EntryDataType As String, _
                                            ByVal EntryData As String, _
                                            ByVal IDEntryData As String) As String
        ' definir los objetos
        Dim bpm As New GattacaBPMServices9000
        Dim result As String

        ' actualizar la url del servicio
        bpm.Url = PublicFunction.getSettingValue("BPMServices.bpmservices")

        ' iniciar el proceso
        result = bpm.AttachDataToActivityInstance(applicationCredentials.ClientName, applicationCredentials.UserID, _
                                            CLng(IdActivityInstance), idProcessInstance, EntryDataType, EntryData, IDEntryData).ToString

        ' retornar
        Return result

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="applicationCredentials"></param>
    ''' <param name="idProcessInstance"></param>
    ''' <param name="comment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function attachDataToProcessInstance(ByVal applicationCredentials As ApplicationCredentials, _
                                            ByVal idProcessInstance As Long, _
                                            ByVal comment As String) As Boolean
        ' definir los objetos
        Dim bpm As New GattacaBPMServices9000
        Dim result As Boolean

        ' actualizar la url del servicio
        bpm.Url = PublicFunction.getSettingValue("BPMServices.bpmservices")

        ' iniciar el proceso
        result = bpm.AttachCommentToProcessInstance(applicationCredentials.ClientName, applicationCredentials.UserID, _
                                            idProcessInstance, comment)

        ' retornar
        Return result

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="applicationCredentials"></param>
    ''' <param name="IdActivityInstance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getConditions(ByVal applicationCredentials As ApplicationCredentials, _
                                            ByVal IdActivityInstance As String) As Array

        ' definir los obj
        Dim bpm As New GattacaBPMServices9000
        Dim conditions As Array

        ' actualizar la url del servicio
        bpm.Url = PublicFunction.getSettingValue("BPMServices.bpmservices")

        ' crear el instancia del proceso
        conditions = bpm.GetConditionsByActivityInstance(applicationCredentials.ClientName, applicationCredentials.UserID, CLng(IdActivityInstance))

        ' retornar
        Return conditions

    End Function

#End Region

End Class
