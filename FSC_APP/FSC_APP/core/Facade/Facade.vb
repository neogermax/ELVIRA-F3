Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Collections.Generic
Imports System.Data
Imports Gattaca.Application.ExceptionManager
Imports Gattaca.Application.Credentials
Imports System.IO

Public Class Facade

    ' defini el nombre
    Const MODULENAME As String = "Facade"

#Region "Menu"

    ''' <summary>
    ''' Cargar la lista de items de menu existentes
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="textField"></param>
    ''' <param name="url"></param>
    ''' <param name="enabled"></param>
    ''' <param name="sortOrden"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMenuList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                Optional ByVal id As String = "", _
                                Optional ByVal textField As String = "", _
                                Optional ByVal url As String = "", _
                                Optional ByVal enabled As String = "", _
                                Optional ByVal sortOrder As String = "", _
                                Optional ByVal order As String = "sortOrder, textField") As List(Of MenuEntity)
        ' definir los objetos
        Dim objMenu As New MenuDALC

        Try
            ' ejecutar la intruccion
            GetMenuList = objMenu.GetMenuList(objApplicationCredentials, id, textField, url, enabled, sortOrder, order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "GetMenuList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los Items del Menu.")

        Finally

            ' liberando recursos
            objMenu = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Reconstruir los archivos de menu
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="sMenuPath"></param>
    ''' <remarks></remarks>
    Public Sub buildApplicationMenus(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal sMenuPath As String)

        ' definir los objetos
        Dim objMenu As New MenuDALC

        Try
            ' ejecutar la intruccion
            objMenu.buildApplicationMenus(objApplicationCredentials, sMenuPath)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "buildApplicationMenus")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al generar los Items del Menu.")

        Finally

            ' liberando recursos
            objMenu = Nothing

        End Try

    End Sub


    ''' <summary>
    ''' Objetivo:  Construir una Menu y cargarla
    ''' Entradas:  El Id
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="iId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal iId As Integer) As MenuEntity

        ' definir los objetos
        Dim objMenu As New MenuDALC

        Try
            ' ejecutar la intruccion
            GetMenu = objMenu.GetMenu(objApplicationCredentials, iId)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "GetMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Item del Menu.")

        Finally

            ' liberando recursos
            objMenu = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Objetivo:  Agregar un menu
    ''' Entradas:  
    ''' Pre:       Debe estar cargado el objeto.   
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="objMenu"></param>
    ''' <remarks></remarks>
    Public Sub Add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal objMenu As MenuEntity)

        ' definir los objetos
        Dim objMenuDALC As New MenuDALC

        Try
            ' ejecutar la intruccion
            objMenuDALC.Add(objApplicationCredentials, objMenu)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "Add")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al crear un Item del Menu.")

        Finally

            ' liberando recursos
            objMenuDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Objetivo:  Modificar los datos de una Menu
    ''' Entradas:  
    ''' Pre:       Deben estar cargados los valores del objeto.   
    ''' Autor:     Diego Armando Gomez
    ''' Fecha:     09/04/2008
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="objMenu"></param>
    ''' <remarks></remarks>
    Public Sub Update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal objMenu As MenuEntity)

        Dim objMenuDALC As New MenuDALC

        Try
            ' ejecutar la intruccion
            objMenuDALC.Update(objApplicationCredentials, objMenu)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "Update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al crear un Item del Menu.")

        Finally

            ' liberando recursos
            objMenu = Nothing

        End Try

    End Sub

    Public Function createMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal Menu As Menu) As Menu

        Dim objMenuDALC As New MenuDALC

        Try
            ' ejecutar la intruccion
            Return objMenuDALC.createMenu(objApplicationCredentials, Menu)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "createMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al crear un Item del Menu.")

        Finally

            ' liberando recursos
            objMenuDALC = Nothing

        End Try

    End Function

    Public Sub buildApplicationMenus2(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal sMenuPath As String)

        ' definir los objetos
        Dim objMenu As New MenuDALC

        Try
            ' ejecutar la intruccion
            objMenu.buildApplicationMenus2(objApplicationCredentials, sMenuPath)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "buildApplicationMenus")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al generar los Items del Menu.")

        Finally

            ' liberando recursos
            objMenu = Nothing

        End Try

    End Sub

    Public Sub buildApplicationMenus3(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal sMenuPath As String)

        ' definir los objetos
        Dim objMenu As New MenuDALC

        Try
            ' ejecutar la intruccion
            objMenu.buildApplicationMenus3(objApplicationCredentials, sMenuPath)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "buildApplicationMenus")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al generar los Items del Menu.")

        Finally

            ' liberando recursos
            objMenu = Nothing

        End Try

    End Sub

#End Region

#Region "Activity"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo número y diferente id(opcional)
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyActivityNumber(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal number As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim ACTIVITY As New ActivityDALC

        Try

            ' retornar el objeto
            verifyActivityNumber = ACTIVITY.verifyNumber(objApplicationCredentials, number, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyNumber")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el número de Activity. ")

        Finally
            ' liberando recursos
            ACTIVITY = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Activity registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getActivityList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal number As String = "", _
        Optional ByVal title As String = "", _
        Optional ByVal idcomponent As String = "", _
        Optional ByVal componentname As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal idobjective As String = "", _
        Optional ByVal objectivename As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal criticalpathtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of ActivityEntity)

        ' definir los objetos
        Dim Activity As New ActivityDALC

        Try
            ' retornar el objeto
            getActivityList = Activity.getList(objApplicationCredentials, _
             id, _
             idlike, _
             number, _
             title, _
             idcomponent, _
             componentname, _
             idproject, _
             projectname, _
             idobjective, _
             objectivename, _
             description, _
             enabled, _
             enabledtext, _
             criticalpathtext, _
             iduser, _
             username, _
             createdate, _
             order, _
             idKey, _
             isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getActivityList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Activity. ")

        Finally
            ' liberando recursos
            Activity = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Activity
    ''' </summary>
    ''' <param name="Activity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Activity As ActivityEntity) As Long

        ' definir los objetos
        Dim ActivityDALC As New ActivityDALC
        Dim miIdActividad As Long

        Try

            ' retornar el objeto
            miIdActividad = ActivityDALC.add(objApplicationCredentials, Activity)


            'Se recorre la lista de objectivos por actividad
            For Each ObjectiveByActivity As ObjectiveByActivityEntity In Activity.OBJECTIVEBYACTIVITYLIST
                'Por cada elemento de la lista
                'Se agrega el id de la Actividad 
                ObjectiveByActivity.idactivity = miIdActividad
                'Se llama al metodo que almacena la informacion de las Componentes del Programa por idea.
                Me.addObjetiveByActivity(objApplicationCredentials, ObjectiveByActivity)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Activity. ")

        Finally
            ' liberando recursos
            ActivityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Activity por el Id
    ''' </summary>
    ''' <param name="idActivity"></param>
    ''' <remarks></remarks>
    Public Function loadActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idActivity As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As ActivityEntity

        ' definir los objetos
        Dim ActivityDALC As New ActivityDALC

        Try

            ' retornar el objeto
            loadActivity = ActivityDALC.load(objApplicationCredentials, idActivity, consultLastVersion)
            'Se llama al metodo que pemite cargar la lista objectivos por Actividad
            loadActivity.OBJECTIVEBYACTIVITYLIST = getObjectiveByActivityList(objApplicationCredentials, , loadActivity.idKey, , )
            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Activity. ")

        Finally
            ' liberando recursos
            ActivityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Activity
    ''' </summary>
    ''' <param name="Activity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Activity As ActivityEntity) As Long

        ' definir los objetos
        Dim ActivityDALC As New ActivityDALC

        Try

            ' retornar el objeto
            updateActivity = ActivityDALC.update(objApplicationCredentials, Activity)

            'Se elimina la informacion existente de los Objectivos  para la actividad actual
            Me.deleteAllObjectiveByActivity(objApplicationCredentials, Activity.idKey)
            'Se recorre la lista de objectivos para la actividad actual
            For Each ObjectiveByActivity As ObjectiveByActivityEntity In Activity.OBJECTIVEBYACTIVITYLIST
                'Por cada elemento de la lista
                'Se agrega el id de la Actividad 
                ObjectiveByActivity.idactivity = Activity.idKey
                'Se llama al metodo que almacena la informacion de las Componentes del Programa por idea.
                Me.addObjetiveByActivity(objApplicationCredentials, ObjectiveByActivity)
            Next



            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Activity. ")

        Finally
            ' liberando recursos
            ActivityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Activity de una forma
    ''' </summary>
    ''' <param name="idActivity"></param>
    ''' <remarks></remarks>
    Public Sub deleteActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idActivity As Integer, _
        ByVal idKey As Integer)

        ' definir los objetos
        Dim ActivityDALC As New ActivityDALC

        Try

            ' retornar el objeto
            ActivityDALC.delete(objApplicationCredentials, idActivity, idKey)

            'Se elimina la informacion existente de las objectivos especificas para la actividad actual
            Me.deleteAllObjectiveByActivity(objApplicationCredentials, idActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Activity. ")

        Finally
            ' liberando recursos
            ActivityDALC = Nothing

        End Try

    End Sub


#End Region

#Region "AccumulationIndicatorSet"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyAccumulationIndicatorSetCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim objAccumulationIndicatorSetDALC As New AccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            verifyAccumulationIndicatorSetCode = objAccumulationIndicatorSetDALC.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            objAccumulationIndicatorSetDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de AccumulationIndicatorSet registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getAccumulationIndicatorSetList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idindicator As String = "", _
        Optional ByVal indicatorcode As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of AccumulationIndicatorSetEntity)

        ' definir los objetos
        Dim AccumulationIndicatorSet As New AccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            getAccumulationIndicatorSetList = AccumulationIndicatorSet.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idindicator, _
             indicatorcode, _
             code, _
             description, _
             name, _
             iduser, _
             username, _
             createdate, _
             order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getAccumulationIndicatorSetList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de AccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            AccumulationIndicatorSet = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo AccumulationIndicatorSet
    ''' </summary>
    ''' <param name="AccumulationIndicatorSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addAccumulationIndicatorSet(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal AccumulationIndicatorSet As AccumulationIndicatorSetEntity) As Long

        ' definir los objetos
        Dim AccumulationIndicatorSetDALC As New AccumulationIndicatorSetDALC
        Dim objIndicatorByAccumulationIndicatorSetDALC As New IndicatorByAccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            addAccumulationIndicatorSet = AccumulationIndicatorSetDALC.add(objApplicationCredentials, AccumulationIndicatorSet)

            ' Guardar la lista de indicadores asociados
            For Each objIndicatorByAccumulationIndicatorSet As IndicatorByAccumulationIndicatorSetEntity In AccumulationIndicatorSet.INDICATORBYACCUMULATIONINDICATORSETLIST
                objIndicatorByAccumulationIndicatorSet.idaccumulationindicatorset = addAccumulationIndicatorSet
                objIndicatorByAccumulationIndicatorSetDALC.add(objApplicationCredentials, objIndicatorByAccumulationIndicatorSet)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addAccumulationIndicatorSet")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un AccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            AccumulationIndicatorSetDALC = Nothing
            objIndicatorByAccumulationIndicatorSetDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un AccumulationIndicatorSet por el Id
    ''' </summary>
    ''' <param name="idAccumulationIndicatorSet"></param>
    ''' <remarks></remarks>
    Public Function loadAccumulationIndicatorSet(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAccumulationIndicatorSet As Integer, _
        Optional ByVal idindicator As Integer = 0) As AccumulationIndicatorSetEntity

        ' definir los objetos
        Dim AccumulationIndicatorSetDALC As New AccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            loadAccumulationIndicatorSet = AccumulationIndicatorSetDALC.load(objApplicationCredentials, idAccumulationIndicatorSet, idindicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadAccumulationIndicatorSet")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un AccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            AccumulationIndicatorSetDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo AccumulationIndicatorSet
    ''' </summary>
    ''' <param name="AccumulationIndicatorSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateAccumulationIndicatorSet(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal AccumulationIndicatorSet As AccumulationIndicatorSetEntity) As Long

        ' definir los objetos
        Dim AccumulationIndicatorSetDALC As New AccumulationIndicatorSetDALC
        Dim objIndicatorByAccumulationIndicatorSetDALC As New IndicatorByAccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            updateAccumulationIndicatorSet = AccumulationIndicatorSetDALC.update(objApplicationCredentials, AccumulationIndicatorSet)

            'Borrrar la lista de indicadores asociados
            objIndicatorByAccumulationIndicatorSetDALC.delete(objApplicationCredentials, 0, AccumulationIndicatorSet.id)

            ' Guardar la lista de indicadores asociados
            For Each objIndicatorByAccumulationIndicatorSet As IndicatorByAccumulationIndicatorSetEntity In AccumulationIndicatorSet.INDICATORBYACCUMULATIONINDICATORSETLIST
                objIndicatorByAccumulationIndicatorSetDALC.add(objApplicationCredentials, objIndicatorByAccumulationIndicatorSet)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateAccumulationIndicatorSet")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un AccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            AccumulationIndicatorSetDALC = Nothing
            objIndicatorByAccumulationIndicatorSetDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el AccumulationIndicatorSet de una forma
    ''' </summary>
    ''' <param name="idAccumulationIndicatorSet"></param>
    ''' <remarks></remarks>
    Public Sub deleteAccumulationIndicatorSet(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAccumulationIndicatorSet As Integer)

        ' definir los objetos
        Dim AccumulationIndicatorSetDALC As New AccumulationIndicatorSetDALC
        Dim objIndicatorByAccumulationIndicatorSetDALC As New IndicatorByAccumulationIndicatorSetDALC

        Try

            'Borrrar la lista de indicadores asociados
            objIndicatorByAccumulationIndicatorSetDALC.delete(objApplicationCredentials, 0, idAccumulationIndicatorSet)

            ' retornar el objeto
            AccumulationIndicatorSetDALC.delete(objApplicationCredentials, idAccumulationIndicatorSet)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteAccumulationIndicatorSet")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un AccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            AccumulationIndicatorSetDALC = Nothing
            objIndicatorByAccumulationIndicatorSetDALC = Nothing

        End Try

    End Sub

#End Region

#Region "IndicatorByAccumulationIndicatorSet"

    ''' <summary>
    ''' Obtener la lista de IndicatorByAccumulationIndicatorSet registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getIndicatorByAccumulationIndicatorSetList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idaccumulationindicatorset As String = "", _
        Optional ByVal idindicator As String = "", _
        Optional ByVal indicatorcode As String = "", _
        Optional ByVal order As String = "") As List(Of IndicatorByAccumulationIndicatorSetEntity)

        ' definir los objetos
        Dim IndicatorByAccumulationIndicatorSet As New IndicatorByAccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            getIndicatorByAccumulationIndicatorSetList = IndicatorByAccumulationIndicatorSet.getList(objApplicationCredentials, _
             id, _
             idaccumulationindicatorset, _
             idindicator, _
             indicatorcode, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getIndicatorByAccumulationIndicatorSetList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de IndicatorByAccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            IndicatorByAccumulationIndicatorSet = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo IndicatorByAccumulationIndicatorSet
    ''' </summary>
    ''' <param name="IndicatorByAccumulationIndicatorSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addIndicatorByAccumulationIndicatorSet(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal IndicatorByAccumulationIndicatorSet As IndicatorByAccumulationIndicatorSetEntity) As Long

        ' definir los objetos
        Dim IndicatorByAccumulationIndicatorSetDALC As New IndicatorByAccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            addIndicatorByAccumulationIndicatorSet = IndicatorByAccumulationIndicatorSetDALC.add(objApplicationCredentials, IndicatorByAccumulationIndicatorSet)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addIndicatorByAccumulationIndicatorSet")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un IndicatorByAccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            IndicatorByAccumulationIndicatorSetDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un IndicatorByAccumulationIndicatorSet por el Id
    ''' </summary>
    ''' <param name="idIndicatorByAccumulationIndicatorSet"></param>
    ''' <remarks></remarks>
    Public Function loadIndicatorByAccumulationIndicatorSet(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIndicatorByAccumulationIndicatorSet As Integer) As IndicatorByAccumulationIndicatorSetEntity

        ' definir los objetos
        Dim IndicatorByAccumulationIndicatorSetDALC As New IndicatorByAccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            loadIndicatorByAccumulationIndicatorSet = IndicatorByAccumulationIndicatorSetDALC.load(objApplicationCredentials, idIndicatorByAccumulationIndicatorSet)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorByAccumulationIndicatorSet")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un IndicatorByAccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            IndicatorByAccumulationIndicatorSetDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo IndicatorByAccumulationIndicatorSet
    ''' </summary>
    ''' <param name="IndicatorByAccumulationIndicatorSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateIndicatorByAccumulationIndicatorSet(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal IndicatorByAccumulationIndicatorSet As IndicatorByAccumulationIndicatorSetEntity) As Long

        ' definir los objetos
        Dim IndicatorByAccumulationIndicatorSetDALC As New IndicatorByAccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            updateIndicatorByAccumulationIndicatorSet = IndicatorByAccumulationIndicatorSetDALC.update(objApplicationCredentials, IndicatorByAccumulationIndicatorSet)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateIndicatorByAccumulationIndicatorSet")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un IndicatorByAccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            IndicatorByAccumulationIndicatorSetDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el IndicatorByAccumulationIndicatorSet de una forma
    ''' </summary>
    ''' <param name="idIndicatorByAccumulationIndicatorSet"></param>
    ''' <remarks></remarks>
    Public Sub deleteIndicatorByAccumulationIndicatorSet(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIndicatorByAccumulationIndicatorSet As Integer, _
                            Optional ByVal idindicator As Integer = 0)

        ' definir los objetos
        Dim IndicatorByAccumulationIndicatorSetDALC As New IndicatorByAccumulationIndicatorSetDALC

        Try

            ' retornar el objeto
            IndicatorByAccumulationIndicatorSetDALC.delete(objApplicationCredentials, idIndicatorByAccumulationIndicatorSet, idindicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteIndicatorByAccumulationIndicatorSet")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un IndicatorByAccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            IndicatorByAccumulationIndicatorSetDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ObjectiveByActivity"


    ''' <summary> 
    ''' Registar un nuevo ProgramComponentByIdea
    ''' </summary>
    ''' <param name="ObjectiveByActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addObjetiveByActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ObjectiveByActivity As ObjectiveByActivityEntity) As Long

        ' definir los objetos
        Dim ObjectiveByActivityDALC As New ObjectiveByActivitDALC

        Try

            ' retornar el objeto
            addObjetiveByActivity = ObjectiveByActivityDALC.add(objApplicationCredentials, ObjectiveByActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addObjetiveByActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ObjetiveByActivity. ")

        Finally
            ' liberando recursos
            ObjectiveByActivityDALC = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Obtener la lista de ProgramComponentByIdea registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getObjectiveByActivityList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idactivity As String = "", _
        Optional ByVal idobjective As String = "", _
        Optional ByVal order As String = "") As List(Of ObjectiveByActivityEntity)

        ' definir los objetos
        Dim ObjectiveByActivity As New ObjectiveByActivitDALC

        Try

            ' retornar el objeto
            getObjectiveByActivityList = ObjectiveByActivity.getList(objApplicationCredentials, _
             id, _
             idactivity, _
             idobjective, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProgramComponentByIdeaList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            ObjectiveByActivity = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra la informacion de las Componentes del Programa de una idea determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idActivity">identificador de la idea</param>
    ''' <remarks></remarks>
    Public Sub deleteAllObjectiveByActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idActivity As Integer)

        ' definir los objetos
        Dim ObjectiveByActivitDALC As New ObjectiveByActivitDALC

        Try
            ' retornar el objeto
            ObjectiveByActivitDALC.deleteAll(objApplicationCredentials, idActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProgramComponentByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            ObjectiveByActivitDALC = Nothing

        End Try

    End Sub
#End Region

#Region "Component"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyComponentCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim component As New ComponentDALC

        Try

            ' retornar el objeto
            verifyComponentCode = component.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            component = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Component registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getComponentList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal idobjective As String = "", _
        Optional ByVal objectivename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of ComponentEntity)

        ' definir los objetos
        Dim Component As New ComponentDALC

        Try

            ' retornar el objeto
            getComponentList = Component.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             idproject, _
             projectname, _
             idobjective, _
             objectivename, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             order, _
             idKey, _
             isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getComponentList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Component. ")

        Finally
            ' liberando recursos
            Component = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Component
    ''' </summary>
    ''' <param name="Component"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addComponent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Component As ComponentEntity) As Long

        ' definir los objetos
        Dim ComponentDALC As New ComponentDALC

        Try

            ' retornar el objeto
            addComponent = ComponentDALC.add(objApplicationCredentials, Component)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addComponent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Component. ")

        Finally
            ' liberando recursos
            ComponentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Component por el Id
    ''' </summary>
    ''' <param name="idComponent"></param>
    ''' <remarks></remarks>
    Public Function loadComponent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idComponent As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As ComponentEntity

        ' definir los objetos
        Dim ComponentDALC As New ComponentDALC

        Try

            ' retornar el objeto
            loadComponent = ComponentDALC.load(objApplicationCredentials, idComponent, consultLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadComponent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Component. ")

        Finally
            ' liberando recursos
            ComponentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Component
    ''' </summary>
    ''' <param name="Component"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateComponent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Component As ComponentEntity) As Long

        ' definir los objetos
        Dim ComponentDALC As New ComponentDALC

        Try

            ' retornar el objeto
            updateComponent = ComponentDALC.update(objApplicationCredentials, Component)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateComponent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Component. ")

        Finally
            ' liberando recursos
            ComponentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Component de una forma
    ''' </summary>
    ''' <param name="idComponent"></param>
    ''' <remarks></remarks>
    Public Sub deleteComponent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idComponent As Integer, _
        ByVal idKey As Integer)

        ' definir los objetos
        Dim ComponentDALC As New ComponentDALC

        Try

            ' retornar el objeto
            ComponentDALC.delete(objApplicationCredentials, idComponent, idKey)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteComponent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Component. ")

        Finally
            ' liberando recursos
            ComponentDALC = Nothing

        End Try

    End Sub


    ''' <summary>
    ''' Consulta La version de un Proyecto
    ''' </summary>
    ''' <param name="idObjective"></param>
    ''' <remarks></remarks>
    Public Function ComponentPhaseProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idObjective As Integer) As Long

        ' definir los objetos
        Dim ComponentDALC As New ComponentDALC

        Try

            ' retornar el objeto
            ComponentPhaseProject = ComponentDALC.PhaseProject(objApplicationCredentials, idObjective)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "ComponentVersionProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al consultar la fase del proyecto. ")

        Finally
            ' liberando recursos
            ComponentDALC = Nothing

        End Try
    End Function

#End Region

#Region "ComponentByRisk"

    ''' <summary>
    ''' Obtener la lista de ComponentByRisk registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getComponentByRiskList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idrisk As String = "", _
        Optional ByVal idcomponent As String = "", _
        Optional ByVal order As String = "") As List(Of ComponentByRiskEntity)

        ' definir los objetos
        Dim ComponentByRisk As New ComponentByRiskDALC

        Try

            ' retornar el objeto
            getComponentByRiskList = ComponentByRisk.getList(objApplicationCredentials, _
             id, _
             idrisk, _
             idcomponent, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getComponentByRiskList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ComponentByRisk. ")

        Finally
            ' liberando recursos
            ComponentByRisk = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ComponentByRisk
    ''' </summary>
    ''' <param name="ComponentByRisk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addComponentByRisk(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ComponentByRisk As ComponentByRiskEntity) As Long

        ' definir los objetos
        Dim ComponentByRiskDALC As New ComponentByRiskDALC

        Try

            ' retornar el objeto
            addComponentByRisk = ComponentByRiskDALC.add(objApplicationCredentials, ComponentByRisk)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addComponentByRisk")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ComponentByRisk. ")

        Finally
            ' liberando recursos
            ComponentByRiskDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ComponentByRisk por el Id
    ''' </summary>
    ''' <param name="idComponentByRisk"></param>
    ''' <remarks></remarks>
    Public Function loadComponentByRisk(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idComponentByRisk As Integer) As ComponentByRiskEntity

        ' definir los objetos
        Dim ComponentByRiskDALC As New ComponentByRiskDALC

        Try

            ' retornar el objeto
            loadComponentByRisk = ComponentByRiskDALC.load(objApplicationCredentials, idComponentByRisk)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadComponentByRisk")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ComponentByRisk. ")

        Finally
            ' liberando recursos
            ComponentByRiskDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ComponentByRisk
    ''' </summary>
    ''' <param name="ComponentByRisk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateComponentByRisk(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ComponentByRisk As ComponentByRiskEntity) As Long

        ' definir los objetos
        Dim ComponentByRiskDALC As New ComponentByRiskDALC

        Try

            ' retornar el objeto
            updateComponentByRisk = ComponentByRiskDALC.update(objApplicationCredentials, ComponentByRisk)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateComponentByRisk")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ComponentByRisk. ")

        Finally
            ' liberando recursos
            ComponentByRiskDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ComponentByRisk de una forma
    ''' </summary>
    ''' <param name="idComponentByRisk"></param>
    ''' <remarks></remarks>
    Public Sub deleteComponentByRisk(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idComponentByRisk As Integer)

        ' definir los objetos
        Dim ComponentByRiskDALC As New ComponentByRiskDALC

        Try

            ' retornar el objeto
            ComponentByRiskDALC.delete(objApplicationCredentials, idComponentByRisk)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteComponentByRisk")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ComponentByRisk. ")

        Finally
            ' liberando recursos
            ComponentByRiskDALC = Nothing

        End Try

    End Sub

#End Region

#Region "City"

    ''' <summary>
    ''' Obtener la lista de City registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCityList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal iddepto As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "") As List(Of CityEntity)

        ' definir los objetos
        Dim City As New CityDALC

        Try

            ' retornar el objeto
            getCityList = City.getList(objApplicationCredentials, _
             id, _
             code, _
             name, _
             iddepto, _
             enabled, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getCityList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de City. ")

        Finally
            ' liberando recursos
            City = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un City por el Id
    ''' </summary>
    ''' <param name="idCity"></param>
    ''' <remarks></remarks>
    Public Function loadCity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idCity As Integer) As CityEntity

        ' definir los objetos
        Dim CityDALC As New CityDALC

        Try

            ' retornar el objeto
            loadCity = CityDALC.load(objApplicationCredentials, idCity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadCity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un City. ")

        Finally
            ' liberando recursos
            CityDALC = Nothing

        End Try

    End Function



#End Region

#Region "Currency"

    ''' <summary>
    ''' Obtener la lista de Currency registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCurrencyList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal priceprefix As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "") As List(Of CurrencyEntity)

        ' definir los objetos
        Dim Currency As New CurrencyDALC

        Try

            ' retornar el objeto
            getCurrencyList = Currency.getList(objApplicationCredentials, _
             id, _
             code, _
             name, _
             priceprefix, _
             enabled, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getCurrencyList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Currency. ")

        Finally
            ' liberando recursos
            Currency = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Currency
    ''' </summary>
    ''' <param name="Currency"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addCurrency(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Currency As CurrencyEntity) As Long

        ' definir los objetos
        Dim CurrencyDALC As New CurrencyDALC

        Try

            ' retornar el objeto
            addCurrency = CurrencyDALC.add(objApplicationCredentials, Currency)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addCurrency")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Currency. ")

        Finally
            ' liberando recursos
            CurrencyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Currency por el Id
    ''' </summary>
    ''' <param name="idCurrency"></param>
    ''' <remarks></remarks>
    Public Function loadCurrency(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idCurrency As Integer) As CurrencyEntity

        ' definir los objetos
        Dim CurrencyDALC As New CurrencyDALC

        Try

            ' retornar el objeto
            loadCurrency = CurrencyDALC.load(objApplicationCredentials, idCurrency)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadCurrency")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Currency. ")

        Finally
            ' liberando recursos
            CurrencyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Currency
    ''' </summary>
    ''' <param name="Currency"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateCurrency(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Currency As CurrencyEntity) As Long

        ' definir los objetos
        Dim CurrencyDALC As New CurrencyDALC

        Try

            ' retornar el objeto
            updateCurrency = CurrencyDALC.update(objApplicationCredentials, Currency)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateCurrency")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Currency. ")

        Finally
            ' liberando recursos
            CurrencyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Currency de una forma
    ''' </summary>
    ''' <param name="idCurrency"></param>
    ''' <remarks></remarks>
    Public Sub deleteCurrency(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idCurrency As Integer)

        ' definir los objetos
        Dim CurrencyDALC As New CurrencyDALC

        Try

            ' retornar el objeto
            CurrencyDALC.delete(objApplicationCredentials, idCurrency)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteCurrency")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Currency. ")

        Finally
            ' liberando recursos
            CurrencyDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Depto"

    ''' <summary>
    ''' Obtener la lista de Depto registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDeptoList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idcountry As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "") As List(Of DeptoEntity)

        ' definir los objetos
        Dim Depto As New DeptoDALC

        Try

            ' retornar el objeto
            getDeptoList = Depto.getList(objApplicationCredentials, _
             id, _
             code, _
             name, _
             idcountry, _
             enabled, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getDeptoList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Depto. ")

        Finally
            ' liberando recursos
            Depto = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Depto por el Id
    ''' </summary>
    ''' <param name="idDepto"></param>
    ''' <remarks></remarks>
    Public Function loadDepto(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDepto As Integer) As DeptoEntity

        ' definir los objetos
        Dim DeptoDALC As New DeptoDALC

        Try

            ' retornar el objeto
            loadDepto = DeptoDALC.load(objApplicationCredentials, idDepto)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadDepto")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Depto. ")

        Finally
            ' liberando recursos
            DeptoDALC = Nothing

        End Try

    End Function



#End Region

#Region "Enterprise"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyEnterpriseCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim ENTERPRISE As New ENTERPRISEDALC

        Try

            ' retornar el objeto
            verifyEnterpriseCode = ENTERPRISE.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            ENTERPRISE = Nothing

        End Try

    End Function

    Public Function getEnterpriseList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ENTERPRISEEntity)

        ' definir los objetos
        Dim ENTERPRISE As New ENTERPRISEDALC

        Try

            ' retornar el objeto
            getEnterpriseList = ENTERPRISE.getList(objApplicationCredentials, id, idlike, code, name, iduser, username, enabled, enabledtext, createdate, order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getEnterpriseList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ENTERPRISE. ")

        Finally
            ' liberando recursos
            ENTERPRISE = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ENTERPRISE
    ''' </summary>
    ''' <param name="ENTERPRISE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addEnterprise(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ENTERPRISE As ENTERPRISEEntity) As Long

        ' definir los objetos
        Dim ENTERPRISEDALC As New ENTERPRISEDALC

        Try

            ' retornar el objeto
            addEnterprise = ENTERPRISEDALC.add(objApplicationCredentials, ENTERPRISE)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addEnterprise")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ENTERPRISE. ")

        Finally
            ' liberando recursos
            ENTERPRISEDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ENTERPRISE por el Id
    ''' </summary>
    ''' <param name="idENTERPRISE"></param>
    ''' <remarks></remarks>
    Public Function loadEnterprise(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idENTERPRISE As Integer) As ENTERPRISEEntity

        ' definir los objetos
        Dim ENTERPRISEDALC As New ENTERPRISEDALC

        Try

            ' retornar el objeto
            loadEnterprise = ENTERPRISEDALC.load(objApplicationCredentials, idENTERPRISE)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadEnterprise")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ENTERPRISE. ")

        Finally
            ' liberando recursos
            ENTERPRISEDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ENTERPRISE
    ''' </summary>
    ''' <param name="ENTERPRISE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateEnterprise(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ENTERPRISE As ENTERPRISEEntity) As Long

        ' definir los objetos
        Dim ENTERPRISEDALC As New ENTERPRISEDALC

        Try

            ' retornar el objeto
            updateEnterprise = ENTERPRISEDALC.update(objApplicationCredentials, ENTERPRISE)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateEnterprise")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ENTERPRISE. ")

        Finally
            ' liberando recursos
            ENTERPRISEDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ENTERPRISE de una forma
    ''' </summary>
    ''' <param name="idENTERPRISE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub deleteEnterprise(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idENTERPRISE As Integer)

        ' definir los objetos
        Dim ENTERPRISEDALC As New ENTERPRISEDALC

        Try

            ' retornar el objeto
            ENTERPRISEDALC.delete(objApplicationCredentials, idENTERPRISE)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteEnterprise")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ENTERPRISE. ")

        Finally
            ' liberando recursos
            ENTERPRISEDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Indicator"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyIndicatorCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Indicator As New IndicatorDALC

        Try

            ' retornar el objeto
            verifyIndicatorCode = Indicator.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            Indicator = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Indicator registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getIndicatorList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal goal As String = "", _
        Optional ByVal greenvalue As String = "", _
        Optional ByVal yellowvalue As String = "", _
        Optional ByVal redvalue As String = "", _
        Optional ByVal assumptions As String = "", _
        Optional ByVal sourceverification As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal entityname As String = "", _
        Optional ByVal levelname As String = "", _
        Optional ByVal levelindicator As String = "", _
        Optional ByVal order As String = "") As List(Of IndicatorEntity)

        ' definir los objetos
        Dim Indicator As New IndicatorDALC

        Try

            ' retornar el objeto
            getIndicatorList = Indicator.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             description, _
             type, _
             goal, _
             greenvalue, _
             yellowvalue, _
             redvalue, _
             assumptions, _
             sourceverification, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             entityname, _
             levelname, _
             levelindicator, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getIndicatorList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Indicator. " & ex.Message)

        Finally
            ' liberando recursos
            Indicator = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Indicator
    ''' </summary>
    ''' <param name="Indicator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Indicator As IndicatorEntity) As Long

        ' definir los objetos
        Dim IndicatorDALC As New IndicatorDALC

        Try

            ' retornar el objeto
            addIndicator = IndicatorDALC.add(objApplicationCredentials, Indicator)

            For Each mdate As MeasurementDateByIndicatorEntity In Indicator.dateList

                ' asociar al indicador
                mdate.idindicator = addIndicator

                ' agregar
                addMeasurementDateByIndicator(objApplicationCredentials, mdate)

            Next


            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Indicator. ")

        Finally
            ' liberando recursos
            IndicatorDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Indicator por el Id
    ''' </summary>
    ''' <param name="idIndicator"></param>
    ''' <remarks></remarks>
    Public Function loadIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIndicator As Integer) As IndicatorEntity

        ' definir los objetos
        Dim IndicatorDALC As New IndicatorDALC

        Try

            ' retornar el objeto
            loadIndicator = IndicatorDALC.load(objApplicationCredentials, idIndicator)

            'Carga la lista de medicion del indicador
            loadIndicator.dateList = getMeasurementDateByIndicatorList(objApplicationCredentials, idindicator:=idIndicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Indicator. ")

        Finally
            ' liberando recursos
            IndicatorDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Indicator
    ''' </summary>
    ''' <param name="Indicator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Indicator As IndicatorEntity) As Long

        ' definir los objetos
        Dim IndicatorDALC As New IndicatorDALC
        Dim oldIndicator As New IndicatorEntity
        Dim repeated As Boolean

        Try

            'cargar el objeto referenciado antes de la actualización
            oldIndicator = loadIndicator(objApplicationCredentials, Indicator.id)

            'Comparar la lista de fechas antigua con la nueva lista
            'Borra de la base de datos las que no estan en la lista nueva
            For Each mdate As MeasurementDateByIndicatorEntity In oldIndicator.dateList
                repeated = False
                For Each mdate1 As MeasurementDateByIndicatorEntity In Indicator.dateList
                    If mdate.Equals(mdate1) Then
                        repeated = True
                    End If
                Next
                If Not repeated Then
                    deleteMeasurementDateByIndicator(objApplicationCredentials, mdate.id)
                End If
            Next

            'Ingresa a la base de datos fechas de la nueva lista
            For Each mdate As MeasurementDateByIndicatorEntity In Indicator.dateList
                If mdate.id = 0 Then
                    mdate.idindicator = Indicator.id
                    addMeasurementDateByIndicator(objApplicationCredentials, mdate)
                End If
            Next

            ' retornar el objeto
            updateIndicator = IndicatorDALC.update(objApplicationCredentials, Indicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Indicator. ")

        Finally
            ' liberando recursos
            IndicatorDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Indicator de una forma
    ''' </summary>
    ''' <param name="idIndicator"></param>
    ''' <remarks></remarks>
    Public Sub deleteIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIndicator As Integer)

        ' definir los objetos
        Dim IndicatorDALC As New IndicatorDALC

        Try

            'Elimina todos los MeasurementDateByIndicator del indicador referenciado
            deleteMeasurementDateByIndicator(objApplicationCredentials, 0, idIndicator:=idIndicator)

            ' retornar el objeto
            IndicatorDALC.delete(objApplicationCredentials, idIndicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Indicator. ")

        Finally
            ' liberando recursos
            IndicatorDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Program"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyProgramCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Program As New ProgramDALC

        Try

            ' retornar el objeto
            verifyProgramCode = Program.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            Program = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Program registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProgramList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal StrategicLinename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ProgramEntity)

        ' definir los objetos
        Dim Program As New ProgramDALC

        Try

            ' retornar el objeto
            getProgramList = Program.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             idStrategicLine, _
             StrategicLinename, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProgramList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Program. ")

        Finally
            ' liberando recursos
            Program = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Program
    ''' </summary>
    ''' <param name="Program"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addProgram(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Program As ProgramEntity) As Long

        ' definir los objetos
        Dim ProgramDALC As New ProgramDALC

        Try

            ' retornar el objeto
            addProgram = ProgramDALC.add(objApplicationCredentials, Program)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addProgram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Program. ")

        Finally
            ' liberando recursos
            ProgramDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Program por el Id
    ''' </summary>
    ''' <param name="idProgram"></param>
    ''' <remarks></remarks>
    Public Function loadProgram(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgram As Integer) As ProgramEntity

        ' definir los objetos
        Dim ProgramDALC As New ProgramDALC

        Try

            ' retornar el objeto
            loadProgram = ProgramDALC.load(objApplicationCredentials, idProgram)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadProgram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Program. ")

        Finally
            ' liberando recursos
            ProgramDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Program
    ''' </summary>
    ''' <param name="Program"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProgram(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Program As ProgramEntity) As Long

        ' definir los objetos
        Dim ProgramDALC As New ProgramDALC

        Try

            ' retornar el objeto
            updateProgram = ProgramDALC.update(objApplicationCredentials, Program)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProgram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Program. ")

        Finally
            ' liberando recursos
            ProgramDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Program de una forma
    ''' </summary>
    ''' <param name="idProgram"></param>
    ''' <remarks></remarks>
    Public Sub deleteProgram(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgram As Integer)

        ' definir los objetos
        Dim ProgramDALC As New ProgramDALC

        Try

            ' retornar el objeto
            ProgramDALC.delete(objApplicationCredentials, idProgram)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProgram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Program. ")

        Finally
            ' liberando recursos
            ProgramDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Managment"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyManagmentCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Managment As New MANAGEMENTDALC

        Try

            ' retornar el objeto
            verifyManagmentCode = Managment.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            Managment = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo MANAGEMENT
    ''' </summary>
    ''' <param name="MANAGEMENT"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addManagement(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal MANAGEMENT As MANAGEMENTEntity) As Long

        ' definir los objetos
        Dim MANAGEMENTDALC As New MANAGEMENTDALC

        Try

            ' retornar el objeto
            addManagement = MANAGEMENTDALC.add(objApplicationCredentials, MANAGEMENT)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addManagement")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un MANAGEMENT. ")

        Finally
            ' liberando recursos
            MANAGEMENTDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo MANAGEMENT
    ''' </summary>
    ''' <param name="MANAGEMENT"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateManagement(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal MANAGEMENT As MANAGEMENTEntity) As Long

        ' definir los objetos
        Dim MANAGEMENTDALC As New MANAGEMENTDALC

        Try

            ' retornar el objeto
            updateManagement = MANAGEMENTDALC.update(objApplicationCredentials, MANAGEMENT)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateManagement")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un MANAGEMENT. ")

        Finally
            ' liberando recursos
            MANAGEMENTDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el MANAGEMENT de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteManagement(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMANAGEMENT As Integer) As Long

        ' definir los objetos
        Dim MANAGEMENTDALC As New MANAGEMENTDALC

        Try

            ' retornar el objeto
            deleteManagement = MANAGEMENTDALC.delete(objApplicationCredentials, idMANAGEMENT)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteManagement")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un MANAGEMENT. ")

        Finally
            ' liberando recursos
            MANAGEMENTDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un MANAGEMENT por el Id
    ''' </summary>
    ''' <param name="idMANAGEMENT"></param>
    ''' <remarks></remarks>
    Public Function loadManagement(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMANAGEMENT As Integer) As MANAGEMENTEntity

        ' definir los objetos
        Dim MANAGEMENTDALC As New MANAGEMENTDALC

        Try

            ' retornar el objeto
            loadManagement = MANAGEMENTDALC.load(objApplicationCredentials, idMANAGEMENT)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadManagement")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un MANAGMENT. ")

        Finally
            ' liberando recursos
            MANAGEMENTDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="identerprise"></param>
    ''' <param name="enterprisename"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of MANAGEMENTEntity)</returns>
    ''' <remarks></remarks>
    Public Function getManagementList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal identerprise As String = "", _
        Optional ByVal enterprisename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of MANAGEMENTEntity)

        ' definir los objetos
        Dim MANAGEMENTDALC As New MANAGEMENTDALC

        Try

            ' retornar el objeto
            getManagementList = MANAGEMENTDALC.getList(objApplicationCredentials, id, idlike, code, name, identerprise, enterprisename, enabled, enabledtext, iduser, username, createdate, order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getManagementList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de MANAGEMENT. ")

        Finally
            ' liberando recursos
            MANAGEMENTDALC = Nothing

        End Try

    End Function
#End Region

#Region "MeasurementDateByIndicator"

    ''' <summary>
    ''' Obtener la lista de MeasurementDateByIndicator registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeasurementDateByIndicatorList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idindicator As String = "", _
        Optional ByVal measurementdate As String = "", _
        Optional ByVal order As String = "") As List(Of MeasurementDateByIndicatorEntity)

        ' definir los objetos
        Dim MeasurementDateByIndicator As New MeasurementDateByIndicatorDALC

        Try

            ' retornar el objeto
            getMeasurementDateByIndicatorList = MeasurementDateByIndicator.getList(objApplicationCredentials, _
             id, _
             idindicator, _
             measurementdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getMeasurementDateByIndicatorList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de MeasurementDateByIndicator. ")

        Finally
            ' liberando recursos
            MeasurementDateByIndicator = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo MeasurementDateByIndicator
    ''' </summary>
    ''' <param name="MeasurementDateByIndicator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addMeasurementDateByIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal MeasurementDateByIndicator As MeasurementDateByIndicatorEntity) As Long

        ' definir los objetos
        Dim MeasurementDateByIndicatorDALC As New MeasurementDateByIndicatorDALC

        Try

            ' retornar el objeto
            addMeasurementDateByIndicator = MeasurementDateByIndicatorDALC.add(objApplicationCredentials, MeasurementDateByIndicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addMeasurementDateByIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un MeasurementDateByIndicator. ")

        Finally
            ' liberando recursos
            MeasurementDateByIndicatorDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un MeasurementDateByIndicator por el Id
    ''' </summary>
    ''' <param name="idMeasurementDateByIndicator"></param>
    ''' <remarks></remarks>
    Public Function loadMeasurementDateByIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMeasurementDateByIndicator As Integer) As MeasurementDateByIndicatorEntity

        ' definir los objetos
        Dim MeasurementDateByIndicatorDALC As New MeasurementDateByIndicatorDALC

        Try

            ' retornar el objeto
            loadMeasurementDateByIndicator = MeasurementDateByIndicatorDALC.load(objApplicationCredentials, idMeasurementDateByIndicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadMeasurementDateByIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un MeasurementDateByIndicator. ")

        Finally
            ' liberando recursos
            MeasurementDateByIndicatorDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo MeasurementDateByIndicator
    ''' </summary>
    ''' <param name="MeasurementDateByIndicator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateMeasurementDateByIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal MeasurementDateByIndicator As MeasurementDateByIndicatorEntity) As Long

        ' definir los objetos
        Dim MeasurementDateByIndicatorDALC As New MeasurementDateByIndicatorDALC

        Try

            ' retornar el objeto
            updateMeasurementDateByIndicator = MeasurementDateByIndicatorDALC.update(objApplicationCredentials, MeasurementDateByIndicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateMeasurementDateByIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un MeasurementDateByIndicator. ")

        Finally
            ' liberando recursos
            MeasurementDateByIndicatorDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el MeasurementDateByIndicator de una forma
    ''' </summary>
    ''' <param name="idMeasurementDateByIndicator"></param>
    ''' <remarks></remarks>
    Public Sub deleteMeasurementDateByIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMeasurementDateByIndicator As Integer, Optional ByVal idIndicator As Integer = 0)

        ' definir los objetos
        Dim MeasurementDateByIndicatorDALC As New MeasurementDateByIndicatorDALC

        Try

            ' retornar el objeto
            MeasurementDateByIndicatorDALC.delete(objApplicationCredentials, idMeasurementDateByIndicator, idIndicator:=idIndicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteMeasurementDateByIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un MeasurementDateByIndicator. ")

        Finally
            ' liberando recursos
            MeasurementDateByIndicatorDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Mitigation"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyMitigationCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim MITIGATION As New MitigationDALC

        Try

            ' retornar el objeto
            verifyMitigationCode = MITIGATION.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de MITIGATION. ")

        Finally
            ' liberando recursos
            MITIGATION = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Mitigation registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMitigationList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal idrisk As String = "", _
        Optional ByVal riskname As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal impactonrisk As String = "", _
        Optional ByVal idresponsible As String = "", _
        Optional ByVal responsiblename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of MitigationEntity)

        ' definir los objetos
        Dim Mitigation As New MitigationDALC

        Try

            ' retornar el objeto
            getMitigationList = Mitigation.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             idrisk, _
             riskname, _
             name, _
             description, _
             impactonrisk, _
             idresponsible, _
             responsiblename, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             order, _
             idKey, _
             isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getMitigationList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Mitigation. ")

        Finally
            ' liberando recursos
            Mitigation = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Mitigation
    ''' </summary>
    ''' <param name="Mitigation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addMitigation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Mitigation As MitigationEntity) As Long

        ' definir los objetos
        Dim MitigationDALC As New MitigationDALC
        Dim objMitigationByRiskDALC As New MitigationByRiskDALC
        Try

            ' retornar el objeto
            addMitigation = MitigationDALC.add(objApplicationCredentials, Mitigation)

            'Guardar la lista de componentes
            For Each objMitigationByRisk As MitigationByRiskEntity In Mitigation.risklist
                objMitigationByRisk.idmitigation = addMitigation
                objMitigationByRiskDALC.add(objApplicationCredentials, objMitigationByRisk)
            Next


            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addMitigation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Mitigation. ")

        Finally
            ' liberando recursos
            MitigationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Mitigation por el Id
    ''' </summary>
    ''' <param name="idMitigation"></param>
    ''' <remarks></remarks>
    Public Function loadMitigation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMitigation As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As MitigationEntity

        ' definir los objetos
        Dim MitigationDALC As New MitigationDALC

        Try

            ' retornar el objeto
            loadMitigation = MitigationDALC.load(objApplicationCredentials, idMitigation, consultLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadMitigation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Mitigation. ")

        Finally
            ' liberando recursos
            MitigationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Mitigation
    ''' </summary>
    ''' <param name="Mitigation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateMitigation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Mitigation As MitigationEntity) As Long

        ' definir los objetos
        Dim MitigationDALC As New MitigationDALC
        Dim objMitigationByRiskDALC As New MitigationByRiskDALC

        Try

            ' retornar el objeto
            updateMitigation = MitigationDALC.update(objApplicationCredentials, Mitigation)

            'Borrar la lista de mitigacion
            objMitigationByRiskDALC.delete(objApplicationCredentials, Mitigation.id)

            'Guardar la lista de componentes
            For Each objMitigationByRisk As MitigationByRiskEntity In Mitigation.risklist
                objMitigationByRisk.idmitigation = Mitigation.id
                objMitigationByRiskDALC.add(objApplicationCredentials, objMitigationByRisk)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateMitigation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Mitigation. ")

        Finally
            ' liberando recursos
            MitigationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Mitigation de una forma
    ''' </summary>
    ''' <param name="idMitigation"></param>
    ''' <remarks></remarks>
    Public Sub deleteMitigation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMitigation As Integer, _
        ByVal idKey As Integer)

        ' definir los objetos
        Dim MitigationDALC As New MitigationDALC
        Dim objMitigationByRiskDALC As New MitigationByRiskDALC

        Try
            'Borrar la lista de mitigacion
            objMitigationByRiskDALC.delete(objApplicationCredentials, idKey)

            ' retornar el objeto
            MitigationDALC.delete(objApplicationCredentials, idMitigation, idKey)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteMitigation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Mitigation. ")

        Finally
            ' liberando recursos
            MitigationDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Consulta La version de un Proyecto
    ''' </summary>
    ''' <param name="idRisk"></param>
    ''' <remarks></remarks>
    Public Function MitigationPhaseProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idRisk As Integer) As Long

        ' definir los objetos
        Dim MitigationDALC As New MitigationDALC

        Try

            ' retornar el objeto
            MitigationPhaseProject = MitigationDALC.PhaseProject(objApplicationCredentials, idRisk)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "MitigationPhaseProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error la fase del proyecto para la mitigación. ")

        Finally
            ' liberando recursos
            MitigationDALC = Nothing

        End Try

    End Function


    ' '' <summary>
    '''' Obtener la lista de Component registradas en el sistema
    '''' </summary>
    '''' <param name="objApplicationCredentials"></param>
    '''' <param name="order"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function getRiskList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '    Optional ByVal id As String = "", _
    '    Optional ByVal idlike As String = "", _
    '    Optional ByVal code As String = "", _
    '    Optional ByVal name As String = "", _
    '    Optional ByVal description As String = "", _
    '    Optional ByVal whatcanhappen As String = "", _
    '    Optional ByVal riskimpact As String = "", _
    '    Optional ByVal ocurrenceprobability As String = "", _
    '    Optional ByVal enabled As String = "", _
    '    Optional ByVal enabledtext As String = "", _
    '    Optional ByVal iduser As String = "", _
    '    Optional ByVal username As String = "", _
    '    Optional ByVal createdate As String = "", _
    '    Optional ByVal idcomponent As String = "", _
    '    Optional ByVal componentname As String = "", _
    '    Optional ByVal idproject As String = "", _
    '    Optional ByVal order As String = "", _
    '    Optional ByVal idKey As String = "", _
    '    Optional ByVal isLastVersion As String = "") As List(Of ComponentEntity)

    '    ' definir los objetos
    '    Dim Risk As New RiskDALC

    '    Try

    '        ' retornar el objeto
    '        getRiskList = Risk.getList(objApplicationCredentials, _
    '         id, _
    '         idlike, _
    '         code, _
    '         name, _
    '        description, _
    '         whatcanhappen, _
    '         riskimpact, _
    '         ocurrenceprobability, _
    '         enabled, _
    '         enabledtext, _
    '         iduser, _
    '         username, _
    '         createdate, _
    '         idcomponent, _
    '         idproject, _
    '         order, _
    '         idKey, _
    '         isLastVersion)

    '        ' finalizar la transaccion
    '        CtxSetComplete()

    '    Catch ex As Exception

    '        ' cancelar la transaccion
    '        CtxSetAbort()

    '        ' publicar el error
    '        GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getComponentList")
    '        ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

    '        ' subir el error de nivel
    '        Throw New Exception("Error al cargar la lista de Component. ")

    '    Finally
    '        ' liberando recursos
    '        Component = Nothing

    '    End Try

    'End Function
#End Region

#Region "Objective"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyObjectiveCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                ByVal code As String, _
                                Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Objective As New ObjectiveDALC

        Try

            ' retornar el objeto
            verifyObjectiveCode = Objective.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            Objective = Nothing

        End Try
    End Function


    ''' <summary>
    ''' Obtener la lista de Objective registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getObjectiveList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of ObjectiveEntity)

        ' definir los objetos
        Dim Objective As New ObjectiveDALC

        Try

            ' retornar el objeto
            getObjectiveList = Objective.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             idproject, _
             projectname, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
            order, _
            idKey, _
            isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getObjectiveList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Objective. ")

        Finally
            ' liberando recursos
            Objective = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Objective
    ''' </summary>
    ''' <param name="Objective"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addObjective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Objective As ObjectiveEntity) As Long

        ' definir los objetos
        Dim ObjectiveDALC As New ObjectiveDALC

        Try

            ' retornar el objeto
            addObjective = ObjectiveDALC.add(objApplicationCredentials, Objective)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addObjective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Objective. ")

        Finally
            ' liberando recursos
            ObjectiveDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Objective por el Id
    ''' </summary>
    ''' <param name="idObjective"></param>
    ''' <remarks></remarks>
    Public Function loadObjective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idObjective As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As ObjectiveEntity

        ' definir los objetos
        Dim ObjectiveDALC As New ObjectiveDALC

        Try

            ' retornar el objeto
            loadObjective = ObjectiveDALC.load(objApplicationCredentials, idObjective, consultLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadObjective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Objective. ")

        Finally
            ' liberando recursos
            ObjectiveDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Objective
    ''' </summary>
    ''' <param name="Objective"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateObjective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Objective As ObjectiveEntity) As Long

        ' definir los objetos
        Dim ObjectiveDALC As New ObjectiveDALC

        Try

            ' retornar el objeto
            updateObjective = ObjectiveDALC.update(objApplicationCredentials, Objective)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateObjective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Objective. ")

        Finally
            ' liberando recursos
            ObjectiveDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Objective de una forma
    ''' </summary>
    ''' <param name="idObjective"></param>
    ''' <remarks></remarks>
    Public Sub deleteObjective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idObjective As Integer, _
        ByVal idKey As Integer)

        ' definir los objetos
        Dim ObjectiveDALC As New ObjectiveDALC

        Try

            ' retornar el objeto
            ObjectiveDALC.delete(objApplicationCredentials, idObjective, idKey)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteObjective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Objective. ")

        Finally
            ' liberando recursos
            ObjectiveDALC = Nothing

        End Try

    End Sub



    ''' <summary>
    ''' Consulta La version de un Proyecto
    ''' </summary>
    ''' <param name="idproject"></param>
    ''' <remarks></remarks>
    Public Function ObjectiveVersionProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idproject As Integer) As Long
        ' definir los objetos
        Dim ObjectiveDALC As New ObjectiveDALC

        Try

            ' retornar el objeto
            Return ObjectiveDALC.VersionProject(objApplicationCredentials, idproject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "ObjectiveVersionProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al la version del proyecto para un objetivo. ")

        Finally
            ' liberando recursos
            ObjectiveDALC = Nothing

        End Try
    End Function


#End Region

#Region "Perspective"

        ''' <summary> 
        ''' Verifica si ya existe el código
        ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
        ''' </summary>
        ''' <param name="code"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
    Public Function verifyPerspectiveCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Perspective As New PERSPECTIVEDALC

        Try

            ' retornar el objeto
            verifyPerspectiveCode = Perspective.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            Perspective = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo PERSPECTIVE
    ''' </summary>
    ''' <param name="PERSPECTIVE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addPerspective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal PERSPECTIVE As PERSPECTIVEEntity) As Long
        ' definir los objetos
        Dim PERSPECTIVEDALC As New PERSPECTIVEDALC

        Try

            ' retornar el objeto
            addPerspective = PERSPECTIVEDALC.add(objApplicationCredentials, PERSPECTIVE)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addEnterprise")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un PERSPECTIVE. ")

        Finally
            ' liberando recursos
            PERSPECTIVEDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo PERSPECTIVE
    ''' </summary>
    ''' <param name="PERSPECTIVE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updatePerspective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal PERSPECTIVE As PERSPECTIVEEntity) As Long

        ' definirlos objetos
        Dim PERSPECTIVEDALC As New PERSPECTIVEDALC

        Try
            ' retornar el objeto
            updatePerspective = PERSPECTIVEDALC.update(objApplicationCredentials, PERSPECTIVE)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updatePerspective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el PERSPECTIVE. " & ex.Message)

        Finally
            ' liberando recursos
            PERSPECTIVEDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el PERSPECTIVE de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deletePerspective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idPERSPECTIVE As Integer) As Long

        ' definir los objetos
        Dim PERSPECTIVEDALC As New PERSPECTIVEDALC

        Try

            ' retornar el objeto
            deletePerspective = PERSPECTIVEDALC.delete(objApplicationCredentials, idPERSPECTIVE)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteEnterprise")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un PERSPECTIVE. ")

        Finally
            ' liberando recursos
            PERSPECTIVEDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un PERSPECTIVE por el Id
    ''' </summary>
    ''' <param name="idPERSPECTIVE"></param>
    ''' <remarks></remarks>
    Public Function loadPerspective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idPERSPECTIVE As Integer) As PERSPECTIVEEntity

        ' definir los objetos
        Dim PERSPECTIVEDALC As New PERSPECTIVEDALC

        Try

            ' retornar el objeto
            loadPerspective = PERSPECTIVEDALC.load(objApplicationCredentials, idPERSPECTIVE)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadPerspective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un PERSPECTIVE. ")

        Finally
            ' liberando recursos
            PERSPECTIVEDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="identerprise"></param>
    ''' <param name="enterprisename"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of PERSPECTIVEEntity)</returns>
    ''' <remarks></remarks>
    Public Function getPerspectiveList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal identerprise As String = "", _
        Optional ByVal enterprisename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of PERSPECTIVEEntity)

        ' definir los objetos
        Dim PERSPECTIVE As New PERSPECTIVEDALC

        Try

            ' retornar el objeto
            getPerspectiveList = PERSPECTIVE.getList(objApplicationCredentials, id, idlike, code, name, identerprise, enterprisename, enabled, enabledtext, iduser, username, createdate, order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getPerspectiveList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de PERSPECTIVE. ")

        Finally
            ' liberando recursos
            PERSPECTIVE = Nothing

        End Try

    End Function

#End Region

#Region "StrategicLine"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyStrategicLineCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim StrategicLine As New STRATEGICLINEDALC

        Try

            ' retornar el objeto
            verifyStrategicLineCode = StrategicLine.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            StrategicLine = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo StrategicLine
    ''' </summary>
    ''' <param name="StrategicLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addStrategicLine(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal StrategicLine As StrategicLineEntity) As Long

        ' definir los objetos
        Dim StrategicLineDALC As New STRATEGICLINEDALC

        Try

            ' retornar el objeto
            addStrategicLine = StrategicLineDALC.add(objApplicationCredentials, StrategicLine)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addStrategicLine")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un StrategicLine. ")

        Finally
            ' liberando recursos
            StrategicLineDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo StrategicLine
    ''' </summary>
    ''' <param name="StrategicLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateStrategicLine(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal StrategicLine As StrategicLineEntity) As Long

        ' definir los objetos
        Dim StrategicLineDALC As New STRATEGICLINEDALC

        Try

            ' retornar el objeto
            updateStrategicLine = StrategicLineDALC.update(objApplicationCredentials, StrategicLine)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateStrategicLine")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un StrategicLine. ")

        Finally
            ' liberando recursos
            StrategicLineDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el StrategicLine de una forma
    ''' </summary>
    ''' <param name="idStrategicLine"></param>
    ''' <remarks></remarks>
    Public Sub deleteStrategicLine(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategicLine As Integer)

        ' definir los objetos
        Dim StrategicLineDALC As New STRATEGICLINEDALC

        Try

            ' retornar el objeto
            StrategicLineDALC.delete(objApplicationCredentials, idStrategicLine)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteStrategicLine")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un StrategicLine. ")

        Finally
            ' liberando recursos
            StrategicLineDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Cargar un StrategicLine por el Id
    ''' </summary>
    ''' <param name="idStrategicLine"></param>
    ''' <remarks></remarks>
    Public Function loadStrategicLine(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategicLine As Integer) As StrategicLineEntity

        ' definir los objetos
        Dim StrategicLineDALC As New STRATEGICLINEDALC

        Try

            ' retornar el objeto
            loadStrategicLine = StrategicLineDALC.load(objApplicationCredentials, idStrategicLine)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadStrategicLine")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un StrategicLine. ")

        Finally
            ' liberando recursos
            StrategicLineDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de StrategicLine registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getStrategicLineList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idstrategicobjective As String = "", _
        Optional ByVal strategicobjectivename As String = "", _
        Optional ByVal idmanagment As String = "", _
        Optional ByVal managementname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of StrategicLineEntity)

        ' definir los objetos
        Dim StrategicLine As New STRATEGICLINEDALC

        Try

            ' retornar el objeto
            getStrategicLineList = StrategicLine.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             idstrategicobjective, _
             strategicobjectivename, _
             idmanagment, _
             managementname, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getStrategicLineList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de StrategicLine. ")

        Finally
            ' liberando recursos
            StrategicLine = Nothing

        End Try

    End Function

#End Region

#Region "Source"

    ''' <summary>
    ''' Obtener la lista de Source registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSourceList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of SourceEntity)

        ' definir los objetos
        Dim Source As New SourceDALC

        Try

            ' retornar el objeto
            getSourceList = Source.getList(objApplicationCredentials, _
             id, _
             code, _
             name, _
             enabled, _
             iduser, _
             createdate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getSourceList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de fuentes.")

        Finally
            ' liberando recursos
            Source = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Source
    ''' </summary>
    ''' <param name="Source"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addSource(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Source As SourceEntity) As Long

        ' definir los objetos
        Dim SourceDALC As New SourceDALC

        Try

            ' retornar el objeto
            addSource = SourceDALC.add(objApplicationCredentials, Source)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addSource")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar una fuente.")

        Finally
            ' liberando recursos
            SourceDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Source por el Id
    ''' </summary>
    ''' <param name="idSource"></param>
    ''' <remarks></remarks>
    Public Function loadSource(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSource As Integer) As SourceEntity

        ' definir los objetos
        Dim SourceDALC As New SourceDALC

        Try

            ' retornar el objeto
            loadSource = SourceDALC.load(objApplicationCredentials, idSource)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadSource")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una fuente.")

        Finally
            ' liberando recursos
            SourceDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Source
    ''' </summary>
    ''' <param name="Source"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateSource(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Source As SourceEntity) As Long

        ' definir los objetos
        Dim SourceDALC As New SourceDALC

        Try

            ' retornar el objeto
            updateSource = SourceDALC.update(objApplicationCredentials, Source)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateSource")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar una fuente.")

        Finally
            ' liberando recursos
            SourceDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Source de una forma
    ''' </summary>
    ''' <param name="idSource"></param>
    ''' <remarks></remarks>
    Public Sub deleteSource(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSource As Integer)

        ' definir los objetos
        Dim SourceDALC As New SourceDALC

        Try

            ' retornar el objeto
            SourceDALC.delete(objApplicationCredentials, idSource)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteSource")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una fuente.")

        Finally
            ' liberando recursos
            SourceDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Project"

    ''' <summary>
    ''' Permite consultar todos los proyectos que no esten en una fase deerminada
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idphase"></param>
    ''' <param name="enabled"></param>
    ''' <param name="order"></param>
    ''' <param name="isLastVersion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProjectListNotInPhase(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idphase As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of ProjectEntity)

        ' definir los objetos
        Dim Project As New ProjectDALC

        Try

            ' retornar el objeto
            getProjectListNotInPhase = Project.getListNotInPhase(objApplicationCredentials, idphase, enabled, order, isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project.")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try

    End Function



    ''' <summary>
    ''' Verifica que la entidad no este aprovada
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="EntryData"></param>
    ''' <param name="idEntryData"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyapproveproject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal EntryData As String, _
     ByVal idEntryData As String, _
        ByVal Status As String) As Boolean
        ' definir los objetos
        Dim Project As New ProjectDALC

        Try

            ' retornar el objeto
            verifyapproveproject = Project.verifyapprove(objApplicationCredentials, EntryData, idEntryData, Status)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyapproveproject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar si el proyecto ya esta aprovado. ")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyProjectCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Project As New ProjectDALC

        Try

            ' retornar el objeto
            verifyProjectCode = Project.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Project registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProjectList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal ididea As String = "", _
        Optional ByVal ideaname As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal objective As String = "", _
        Optional ByVal antecedent As String = "", _
        Optional ByVal justification As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal duration As String = "", _
        Optional ByVal zonedescription As String = "", _
        Optional ByVal population As String = "", _
        Optional ByVal strategicdescription As String = "", _
        Optional ByVal results As String = "", _
        Optional ByVal source As String = "", _
        Optional ByVal purpose As String = "", _
        Optional ByVal totalcost As String = "", _
        Optional ByVal fsccontribution As String = "", _
        Optional ByVal counterpartvalue As String = "", _
        Optional ByVal effectivebudget As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal idphase As String = "", _
        Optional ByVal phasename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal StrategicLinename As String = "", _
        Optional ByVal idProgram As String = "", _
        Optional ByVal Programname As String = "", _
        Optional ByVal idProgramComponent As String = "", _
        Optional ByVal ProgramComponentname As String = "", _
        Optional ByVal currentactivityname As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal completiondate As String = "", _
        Optional ByVal isLastVersion As String = "", _
        Optional ByVal fromIndicador As Integer = 0) As List(Of ProjectEntity)

        ' definir los objetos
        Dim Project As New ProjectDALC

        Try

            ' retornar el objeto
            getProjectList = Project.getList(objApplicationCredentials, _
             id, _
             idlike, _
             ididea, _
             ideaname, _
             code, _
             name, _
             objective, _
             antecedent, _
             justification, _
             begindate, _
             duration, _
             zonedescription, _
             population, _
             strategicdescription, _
             results, _
             source, _
             purpose, _
             totalcost, _
             fsccontribution, _
             counterpartvalue, _
             effectivebudget, _
             attachment, _
             idphase, _
             phasename, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             idStrategicLine, _
             StrategicLinename, _
             idProgram, _
             Programname, _
             idProgramComponent, _
             ProgramComponentname, _
             currentactivityname, _
             createdate, _
             order, _
             idKey, _
             isLastVersion, _
            fromIndicador)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project. ")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga la lista de proyectos
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="enabled"></param>
    ''' <param name="order"></param>
    ''' <param name="isLastVersion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProjectListToDropDownList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal enabled As String = "", _
    Optional ByVal order As String = "", _
    Optional ByVal isLastVersion As String = "") As List(Of ProjectEntity)

        ' definir los objetos
        Dim Project As New ProjectDALC

        Try

            ' retornar el objeto
            getProjectListToDropDownList = Project.getListToDropDownList(objApplicationCredentials, _
             enabled, _
             order, _
             isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project. ")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Permite consultar todos los proyectos que no esten en una fase deerminada
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idphase"></param>
    ''' <param name="enabled"></param>
    ''' <param name="order"></param>
    ''' <param name="isLastVersion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProjectListNotInPhaseapproval(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idphase As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of IdeaEntity)

        ' definir los objetos
        Dim Project As New ProjectDALC

        Try

            ' retornar el objeto
            getProjectListNotInPhaseapproval = Project.getListNotInPhaseapprovalrecord(objApplicationCredentials, idphase, enabled, order, isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project.")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite consultar TOdAS LAS IDEA APROVADAS PARA EL OTRO SI
    ''' TODO: 37 ORIGEN DE FUNCION CREACION DE FUNCION PARA OTRO SI
    ''' AUTOR:GERMAN RODRIGUEZ 14/06/2013 MGgroup
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idphase"></param>
    ''' <param name="enabled"></param>
    ''' <param name="order"></param>
    ''' <param name="isLastVersion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function getProjectListNotInPhaseOtherYes(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      Optional ByVal idphase As String = "", _
      Optional ByVal enabled As String = "", _
      Optional ByVal order As String = "", _
      Optional ByVal isLastVersion As String = "") As List(Of IdeaEntity)

        ' definir los objetos
        Dim Project As New ProjectDALC

        Try

            ' retornar el objeto
            getProjectListNotInPhaseOtherYes = Project.getListIdeaAprobada(objApplicationCredentials, idphase, enabled, order, isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project.")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try

    End Function
    ' TODO: 37 ORIGEN DE FUNCION CREACION DE FUNCION PARA OTRO SI
    ' AUTOR:GERMAN RODRIGUEZ 14/06/2013 MGgroup
    ' cierre de cambio


    ''' <summary> 
    ''' Registar un nuevo Project
    ''' </summary>
    ''' <param name="Project"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Project As ProjectEntity) As Long

        ' definir los objetos
        Dim ProjectDALC As New ProjectDALC
        Dim objSourceByProjectDALC As New SourceByProjectDALC
        Dim objProjectLocationDALC As New ProjectLocationDALC
        Dim objThirdByProjectDALC As New ThirdByProjectDALC
        Dim objOperatorByProjectDALC As New OperatorByProjectDALC
        Dim objProgramComponentByProjectDALC As New ProgramComponentByProjectDALC
        Dim objPaymentFlowDALC As New PaymentFlowDALC()
        Dim objExplanatoryDALC As New ExplanatoryDALC()

        Try

            ' retornar el objeto
            addProject = ProjectDALC.add(objApplicationCredentials, Project)
           

            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (Project.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In Project.DOCUMENTLIST
                    'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                    If (document.attachfile.Length > 0) Then

                        'Se instancia un objeto de tipo documento por entidad
                        Dim documentByEntity As New DocumentsByEntityEntity()

                        'Se almacena el documento y se recupera su Id
                        documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                        documentByEntity.idnentity = addProject
                        documentByEntity.entityname = Project.GetType.ToString()

                        'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                        Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        document.ISNEW = False
                    End If
                Next
            End If

            'Guardar la lista de fuentes del proyecto projectLocation
            For Each objSourceByProject As SourceByProjectEntity In Project.sourceByProjectList
                objSourceByProject.idproject = addProject
                objSourceByProjectDALC.add(objApplicationCredentials, objSourceByProject)
            Next

            'Guardar la lista de Ubicaciones del proyecto projectLocation
            For Each objProjectLocation As ProjectLocationEntity In Project.projectlocationlist
                objProjectLocation.idproject = addProject
                objProjectLocationDALC.add(objApplicationCredentials, objProjectLocation)
            Next

            'Guardar la lista de terceros del proyecto thirdByProject
            For Each objThirdByProject As ThirdByProjectEntity In Project.thirdbyprojectlist
                objThirdByProject.idproject = addProject
                objThirdByProjectDALC.add(objApplicationCredentials, objThirdByProject)
            Next

            

            'Guardar la lista de Componentes del Programa del proyecto ProgramComponentByProject
            For Each objProgramComponentByProject As ProgramComponentByProjectEntity In Project.ProgramComponentbyprojectlist
                objProgramComponentByProject.idproject = addProject
                objProgramComponentByProjectDALC.add(objApplicationCredentials, objProgramComponentByProject)
            Next



            'Guardar la lista de flujos de pago del proyecto
            For Each objPaymentFlow As PaymentFlowEntity In Project.paymentflowByProjectList
                objPaymentFlow.idproject = addProject
                objPaymentFlowDALC.add(objApplicationCredentials, objPaymentFlow)
            Next

            'Guardar la lista de aclaratorios del proyecto   
            'For Each objExplanatoryEntity As ExplanatoryEntity In Project.explanatoryEntityList
            '    objExplanatoryEntity.idproject = addProject
            '    objExplanatoryDALC.add(objApplicationCredentials, objExplanatoryEntity)
            'Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Project. ")

        Finally
            ' liberando recursos
            ProjectDALC = Nothing
            objProjectLocationDALC = Nothing
            objThirdByProjectDALC = Nothing
            objOperatorByProjectDALC = Nothing
            objProgramComponentByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Project por el Id
    ''' </summary>
    ''' <param name="idProject"></param>
    ''' <remarks></remarks>
    Public Function loadProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProject As Integer, Optional ByVal consultLastVersion As Boolean = True) As ProjectEntity

        ' definir los objetos
        Dim ProjectDALC As New ProjectDALC

        Try

            ' retornar el objeto
            loadProject = ProjectDALC.load(objApplicationCredentials, idProject, consultLastVersion)
            'Se carga la lista de documentos para el registro de idea actual
            loadProject.DOCUMENTSBYIDEALIST = Me.getDocumentsByEntityList(objApplicationCredentials, idnentity:=idProject, entityName:=GetType(ProjectEntity).ToString())

            'Se verifica que existam documentos anexos al registro actual
            If (Not loadProject.DOCUMENTSBYIDEALIST Is Nothing AndAlso loadProject.DOCUMENTSBYIDEALIST.Count > 0) Then
                'Se recorre la lista de identificadores de documentos agregados
                Dim idsDocuments As String = ""
                For Each documentByEntity As DocumentsByEntityEntity In loadProject.DOCUMENTSBYIDEALIST
                    idsDocuments &= documentByEntity.iddocuments.ToString() & ","
                Next
                If (idsDocuments.Length > 0) Then idsDocuments = idsDocuments.Substring(0, idsDocuments.Length - 1)

                'Se carga la lista de documentos requeridos
                loadProject.DOCUMENTLIST = Me.getDocumentsListByEntity(objApplicationCredentials, idsDocuments)


            End If

            loadProject.thirdbyprojectlist = getThirdByProjectList(objApplicationCredentials, , idProject, , , , , )
            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Project. ")

        Finally
            ' liberando recursos
            ProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Project
    ''' </summary>
    ''' <param name="Project"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Project As ProjectEntity, _
        ByVal sRutaOldFile As String, _
        Optional ByVal idPhase As Integer = 0) As Long

        ' definir los objetos
        Dim ProjectDALC As New ProjectDALC
        Dim objSourceByProjectDALC As New SourceByProjectDALC
        Dim objProjectLocationDALC As New ProjectLocationDALC
        Dim objThirdByProjectDALC As New ThirdByProjectDALC
        ' crear objeto flujo de pago
        Dim objPaymentFlowDALC As New PaymentFlowDALC()

        Dim objOperatorByProjectDALC As New OperatorByProjectDALC
        Dim objProgramComponentByProjectDALC As New ProgramComponentByProjectDALC
        Dim DocumentByProjectList As List(Of DocumentsByEntityEntity) = New List(Of DocumentsByEntityEntity)

        Try

            ' retornar el objeto
            updateProject = ProjectDALC.updateNoApprovals(objApplicationCredentials, Project, idPhase)
            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (Project.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In Project.DOCUMENTLIST

                    'Se verifica si el archivo actual es un archivo nuevo
                    If (document.ISNEW) Then

                        'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                        If (document.attachfile.Length > 0) Then

                            'Se instancia un objeto de tipo documento por entidad
                            Dim documentByEntity As New DocumentsByEntityEntity()

                            'Se almacena el documento y se recupera su Id
                            documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                            documentByEntity.idnentity = Project.id
                            documentByEntity.entityname = Project.GetType.ToString()

                            'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                            Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        End If

                        'Se verifica si el archivo ha sido borrado
                    ElseIf (document.ISDELETED) Then

                        'Se llama al metodo que permite eliminar el registro de documentos por entidad
                        Me.deleteAllDocumentsByDocumentAndEntity(objApplicationCredentials, document.id, Project.id, Project.GetType.ToString())

                        'Se llama al metodo que permite eliminar el documento requerido
                        Me.deleteDocuments(objApplicationCredentials, document.id, document.attachfile)

                    End If
                Next
            End If
            
            'If (idPhase = 0) Then

            'Borrar la lista de fuentes del proyecto
            'objSourceByProjectDALC.deleteByProject(objApplicationCredentials, Project.idKey)
            'Guardar la lista de fuentes del proyecto projectLocation
            'For Each objSourceByProject As SourceByProjectEntity In Project.sourceByProjectList
            '    objSourceByProject.idproject = Project.idKey
            '    objSourceByProjectDALC.add(objApplicationCredentials, objSourceByProject)
            'Next

            'Borrar la lista de Ubicaciones del proyecto projectLocation
            objProjectLocationDALC.delete(objApplicationCredentials, 0, Project.idKey)
            'Guardar la lista de Ubicaciones del proyecto projectLocation
            For Each objProjectLocation As ProjectLocationEntity In Project.projectlocationlist
                objProjectLocation.idproject = Project.id
                objProjectLocationDALC.add(objApplicationCredentials, objProjectLocation)
            Next

            

            'Borrar la lista de terceros del proyecto thirdByProject
            objThirdByProjectDALC.delete(objApplicationCredentials, 0, Project.idKey)
            'Guardar la lista de terceros del proyecto thirdByProject
            For Each objThirdByProject As ThirdByProjectEntity In Project.thirdbyprojectlist
                objThirdByProject.idproject = Project.idKey
                objThirdByProjectDALC.add(objApplicationCredentials, objThirdByProject)
            Next

            
            objPaymentFlowDALC.delete(objApplicationCredentials, Project.id)
            'Guardar la lista de flujos de pago del proyecto
            For Each objPaymentFlow As PaymentFlowEntity In Project.paymentflowByProjectList
                objPaymentFlow.idproject = Project.id
                objPaymentFlowDALC.add(objApplicationCredentials, objPaymentFlow)
            Next


            'End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Project. ")

        Finally
            ' liberando recursos
            ProjectDALC = Nothing
            objProjectLocationDALC = Nothing
            objThirdByProjectDALC = Nothing
            objOperatorByProjectDALC = Nothing
            objProgramComponentByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Project VERSION 2.0  modificado por: HERNAN GOMEZ
    ''' </summary>
    ''' <param name="Project"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProjectLastRequirements(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Project As ProjectEntity, _
       ByVal sRutaOldFile As String, _
       Optional ByVal idPhase As Integer = 0) As Long
        Dim ProjectDALC As New ProjectDALC

        Try

            ' retornar el objeto
            updateProjectLastRequirements = ProjectDALC.updateFields(objApplicationCredentials, Project, idPhase)

            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (Project.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In Project.DOCUMENTLIST

                    'Se verifica si el archivo actual es un archivo nuevo
                    If (document.ISNEW) Then

                        'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                        If (document.attachfile.Length > 0) Then

                            'Se instancia un objeto de tipo documento por entidad
                            Dim documentByEntity As New DocumentsByEntityEntity()

                            'Se almacena el documento y se recupera su Id
                            documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                            documentByEntity.idnentity = Project.id
                            documentByEntity.entityname = Project.GetType.ToString()

                            'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                            Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        End If

                        'Se verifica si el archivo ha sido borrado
                    ElseIf (document.ISDELETED) Then

                        'Se llama al metodo que permite eliminar el registro de documentos por entidad
                        Me.deleteAllDocumentsByDocumentAndEntity(objApplicationCredentials, document.id, Project.id, Project.GetType.ToString())

                        'Se llama al metodo que permite eliminar el documento requerido
                        Me.deleteDocuments(objApplicationCredentials, document.id, document.attachfile)

                    End If
                Next
            End If

            

            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProjectLastRequirements")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Project. ")

        Finally
            ' liberando recursos
            'ProjectDALC = Nothing
            'objProjectLocationDALC = Nothing
            'objThirdByProjectDALC = Nothing
            'objOperatorByProjectDALC = Nothing
            'objProgramComponentByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Project de una forma
    ''' </summary>
    ''' <param name="idProject"></param>
    ''' <remarks></remarks>
    Public Sub deleteProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProject As Integer, _
        ByVal idKey As Integer)

        ' definir los objetos
        Dim ProjectDALC As New ProjectDALC
        Dim objSourceByProjectDALC As New SourceByProjectDALC
        Dim objProjectLocationDALC As New ProjectLocationDALC
        Dim objThirdByProjectDALC As New ThirdByProjectDALC
        Dim objOperatorByProjectDALC As New OperatorByProjectDALC
        Dim objProgramComponentByProjectDALC As New ProgramComponentByProjectDALC
        'AUTOR: German Rodriguez MGgroup 29-10-2013
        Dim objpaimentflowDALC As New PaymentFlowDALC
        Dim objexplanatoryDALC As New ExplanatoryDALC


        Try

            'TODO: 60 borrarla lista de pagos anclados al proyecto
            'AUTOR: German Rodriguez MGgroup 29-10-2013
            objpaimentflowDALC.delete(objApplicationCredentials, idProject)
            'borrarla lista de aclaratorias anclados al proyecto
            objexplanatoryDALC.delete(objApplicationCredentials, idProject)
            'TODO: 60 borrarla lista de pagos anclados al proyecto
            'AUTOR: German Rodriguez MGgroup 29-10-2013
            'cierre de la modificacion

            'Borrar la lista de fuentes del proyecto
            objSourceByProjectDALC.deleteByProject(objApplicationCredentials, idProject)

            'Borrar la lista de Ubicaciones del proyecto projectLocation
            objProjectLocationDALC.delete(objApplicationCredentials, 0, idProject)

            'Borrar la lista de terceros del proyecto thirdByProject
            objThirdByProjectDALC.delete(objApplicationCredentials, 0, idProject)

            'Borrar la lista de Operadores del proyecto OperatorByProject
            objOperatorByProjectDALC.delete(objApplicationCredentials, 0, idProject)

            'Borrar la lista de Componentes del Programa del proyecto ProgramComponentByProject
            objProgramComponentByProjectDALC.delete(objApplicationCredentials, 0, idProject)



            ' retornar el objeto
            ProjectDALC.delete(objApplicationCredentials, idProject, idKey)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Project. " & ex.Message)

        Finally
            ' liberando recursos
            ProjectDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Cargar una lista de proyectos sin filtrado, usado para el panel general de proyecto
    ''' </summary>
    ''' <remarks></remarks>
    Public Function getProjectListNoFilter(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As List(Of ProjectEntity)

        ' definir los objetos
        Dim ProjectDALC As New ProjectDALC

        Try

            ' retornar el objeto
            getProjectListNoFilter = ProjectDALC.getListNoFilter(objApplicationCredentials)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectListNoFilter")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de  Project. ")

        Finally
            ' liberando recursos
            ProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite consultar la información de los proyectos
    ''' segun una linea Estrategica determinado
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idStrategicLine">identificador de la linea estrategica</param>
    ''' <param name="enabled">estado del registro</param>
    ''' <param name="order">orden de los campos</param>
    ''' <returns>lista de objetos de tipo ProjectEntity</returns>
    ''' <remarks></remarks>
    Public Function getProjectListByStrategicLine(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "") As List(Of ProjectEntity)

        ' definir los objetos
        Dim Project As New ProjectDALC

        Try
            ' retornar el objeto
            getProjectListByStrategicLine = Project.getListByStrategicLine(objApplicationCredentials, idStrategicLine, enabled, order)
            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Proyectos.")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try

    End Function



    ''' <summary>
    ''' Cambia la fase del proyecto
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="idPhase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangePhases(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                            ByVal idProject As Integer, _
                            ByVal idPhase As Integer) As Long
        ' definir los objetos
        Dim Project As New ProjectDALC

        Try
            ' retornar el objeto
            ChangePhases = Project.ChangePhases(objApplicationCredentials, idProject, idPhase)
            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "ChangePhases")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al actualizar las fases del proyecto.")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try

    End Function


#End Region

#Region "ProjectApprovalRecord"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyProjectApprovalRecordCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim objProjectApprovalRecordDALC As New ProjectApprovalRecordDALC

        Try

            ' retornar el objeto
            verifyProjectApprovalRecordCode = objProjectApprovalRecordDALC.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de REGISTRAR APROBACIÓN DEL PROYECTO. ")

        Finally
            ' liberando recursos
            objProjectApprovalRecordDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de ProjectApprovalRecord registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProjectApprovalRecordList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal approvaldate As String = "", _
        Optional ByVal actnumber As String = "", _
        Optional ByVal approvedvalue As String = "", _
        Optional ByVal approved As String = "", _
        Optional ByVal approvedtext As String = "", _
        Optional ByVal codeapprovedidea As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ProjectApprovalRecordEntity)
        

        ' definir los objetos
        Dim ProjectApprovalRecord As New ProjectApprovalRecordDALC

        Try

            ' retornar el objeto
            getProjectApprovalRecordList = ProjectApprovalRecord.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idproject, _
             projectname, _
             code, _
             comments, _
             attachment, _
             approvaldate, _
             actnumber, _
             approvedvalue, _
             approved, _
             approvedtext, _
             codeapprovedidea, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
   order)


            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectApprovalRecordList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProjectApprovalRecord. ")

        Finally
            ' liberando recursos
            ProjectApprovalRecord = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ProjectApprovalRecord
    ''' </summary>
    ''' <param name="ProjectApprovalRecord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addProjectApprovalRecord(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProjectApprovalRecord As ProjectApprovalRecordEntity) As Long

        ' definir los objetos
        Dim ProjectApprovalRecordDALC As New ProjectApprovalRecordDALC

        Try

            ' retornar el objeto
            addProjectApprovalRecord = ProjectApprovalRecordDALC.add(objApplicationCredentials, ProjectApprovalRecord)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addProjectApprovalRecord")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ProjectApprovalRecord. ")

        Finally
            ' liberando recursos
            ProjectApprovalRecordDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProjectApprovalRecord por el Id
    ''' </summary>
    ''' <param name="idProjectApprovalRecord"></param>
    ''' <remarks></remarks>
    Public Function loadProjectApprovalRecord(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProjectApprovalRecord As Integer) As ProjectApprovalRecordEntity

        ' definir los objetos
        Dim ProjectApprovalRecordDALC As New ProjectApprovalRecordDALC

        Try

            ' retornar el objeto
            loadProjectApprovalRecord = ProjectApprovalRecordDALC.load(objApplicationCredentials, idProjectApprovalRecord)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadProjectApprovalRecord")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProjectApprovalRecord. ")

        Finally
            ' liberando recursos
            ProjectApprovalRecordDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProjectApprovalRecord
    ''' </summary>
    ''' <param name="ProjectApprovalRecord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProjectApprovalRecord(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProjectApprovalRecord As ProjectApprovalRecordEntity) As Long

        ' definir los objetos
        Dim ProjectApprovalRecordDALC As New ProjectApprovalRecordDALC

        Try

            ' retornar el objeto
            updateProjectApprovalRecord = ProjectApprovalRecordDALC.update(objApplicationCredentials, ProjectApprovalRecord)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProjectApprovalRecord")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ProjectApprovalRecord. ")

        Finally
            ' liberando recursos
            ProjectApprovalRecordDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProjectApprovalRecord de una forma
    ''' </summary>
    ''' <param name="idProjectApprovalRecord"></param>
    ''' <remarks></remarks>
    Public Sub deleteProjectApprovalRecord(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProjectApprovalRecord As Integer)

        ' definir los objetos
        Dim ProjectApprovalRecordDALC As New ProjectApprovalRecordDALC

        Try

            ' retornar el objeto
            ProjectApprovalRecordDALC.delete(objApplicationCredentials, idProjectApprovalRecord)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProjectApprovalRecord")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ProjectApprovalRecord. ")

        Finally
            ' liberando recursos
            ProjectApprovalRecordDALC = Nothing

        End Try

    End Sub

#End Region

#Region "SourceByProject"

    ''' <summary>
    ''' Obtener la lista de SourceByProject registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSourceByProjectList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idsource As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal order As String = "") As List(Of SourceByProjectEntity)

        ' definir los objetos
        Dim SourceByProject As New SourceByProjectDALC

        Try

            ' retornar el objeto
            getSourceByProjectList = SourceByProject.getList(objApplicationCredentials, _
             id, _
             idsource, _
             idproject, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getSourceByProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de fuentes por proyecto.")

        Finally
            ' liberando recursos
            SourceByProject = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo SourceByProject
    ''' </summary>
    ''' <param name="SourceByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addSourceByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal SourceByProject As SourceByProjectEntity) As Long

        ' definir los objetos
        Dim SourceByProjectDALC As New SourceByProjectDALC

        Try

            ' retornar el objeto
            addSourceByProject = SourceByProjectDALC.add(objApplicationCredentials, SourceByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addSourceByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar una fuente por proyecto.")

        Finally
            ' liberando recursos
            SourceByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un SourceByProject por el Id
    ''' </summary>
    ''' <param name="idSourceByProject"></param>
    ''' <remarks></remarks>
    Public Function loadSourceByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSourceByProject As Integer) As SourceByProjectEntity

        ' definir los objetos
        Dim SourceByProjectDALC As New SourceByProjectDALC

        Try

            ' retornar el objeto
            loadSourceByProject = SourceByProjectDALC.load(objApplicationCredentials, idSourceByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadSourceByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una fuente por proyecto.")

        Finally
            ' liberando recursos
            SourceByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo SourceByProject
    ''' </summary>
    ''' <param name="SourceByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateSourceByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal SourceByProject As SourceByProjectEntity) As Long

        ' definir los objetos
        Dim SourceByProjectDALC As New SourceByProjectDALC

        Try

            ' retornar el objeto
            updateSourceByProject = SourceByProjectDALC.update(objApplicationCredentials, SourceByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateSourceByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar una fuente por proyecto.")

        Finally
            ' liberando recursos
            SourceByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el SourceByProject de una forma
    ''' </summary>
    ''' <param name="idSourceByProject"></param>
    ''' <remarks></remarks>
    Public Sub deleteSourceByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSourceByProject As Integer)

        ' definir los objetos
        Dim SourceByProjectDALC As New SourceByProjectDALC

        Try

            ' retornar el objeto
            SourceByProjectDALC.delete(objApplicationCredentials, idSourceByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteSourceByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una fuente por proyecto.")

        Finally
            ' liberando recursos
            SourceByProjectDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra el SourceByProject de una forma para un projecto determinado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub deleteAllSourceByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProject As Integer)

        ' definir los objetos
        Dim SourceByProjectDALC As New SourceByProjectDALC

        Try

            ' retornar el objeto
            SourceByProjectDALC.deleteByProject(objApplicationCredentials, idProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteSourceByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una fuente por proyecto.")

        Finally
            ' liberando recursos
            SourceByProjectDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ProjectLocation"

    ''' <summary>
    ''' Obtener la lista de ProjectLocation registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProjectLocationList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal idcity As String = "", _
        Optional ByVal order As String = "") As List(Of ProjectLocationEntity)

        ' definir los objetos
        Dim ProjectLocation As New ProjectLocationDALC

        Try

            ' retornar el objeto
            getProjectLocationList = ProjectLocation.getList(objApplicationCredentials, _
             id, _
             idproject, _
             idcity, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectLocationList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProjectLocation. ")

        Finally
            ' liberando recursos
            ProjectLocation = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ProjectLocation
    ''' </summary>
    ''' <param name="ProjectLocation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addProjectLocation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProjectLocation As ProjectLocationEntity) As Long

        ' definir los objetos
        Dim ProjectLocationDALC As New ProjectLocationDALC

        Try

            ' retornar el objeto
            addProjectLocation = ProjectLocationDALC.add(objApplicationCredentials, ProjectLocation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addProjectLocation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ProjectLocation. ")

        Finally
            ' liberando recursos
            ProjectLocationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProjectLocation por el Id
    ''' </summary>
    ''' <param name="idProjectLocation"></param>
    ''' <remarks></remarks>
    Public Function loadProjectLocation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProjectLocation As Integer) As ProjectLocationEntity

        ' definir los objetos
        Dim ProjectLocationDALC As New ProjectLocationDALC

        Try

            ' retornar el objeto
            loadProjectLocation = ProjectLocationDALC.load(objApplicationCredentials, idProjectLocation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadProjectLocation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProjectLocation. ")

        Finally
            ' liberando recursos
            ProjectLocationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProjectLocation
    ''' </summary>
    ''' <param name="ProjectLocation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProjectLocation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProjectLocation As ProjectLocationEntity) As Long

        ' definir los objetos
        Dim ProjectLocationDALC As New ProjectLocationDALC

        Try

            ' retornar el objeto
            updateProjectLocation = ProjectLocationDALC.update(objApplicationCredentials, ProjectLocation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProjectLocation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ProjectLocation. ")

        Finally
            ' liberando recursos
            ProjectLocationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProjectLocation de una forma
    ''' </summary>
    ''' <param name="idProjectLocation"></param>
    ''' <remarks></remarks>
    Public Sub deleteProjectLocation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProjectLocation As Integer, _
    Optional ByVal idProject As Integer = 0)

        ' definir los objetos
        Dim ProjectLocationDALC As New ProjectLocationDALC

        Try

            ' retornar el objeto
            ProjectLocationDALC.delete(objApplicationCredentials, idProjectLocation, idProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProjectLocation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ProjectLocation. ")

        Finally
            ' liberando recursos
            ProjectLocationDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ThirdByProject"

    ''' <summary>
    ''' Obtener la lista de ThirdByProject registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getThirdByProjectList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal idthird As String = "", _
        Optional ByVal actions As String = "", _
        Optional ByVal experiences As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal order As String = "") As List(Of ThirdByProjectEntity)

        ' definir los objetos
        Dim ThirdByProject As New ThirdByProjectDALC

        Try

            ' retornar el objeto
            getThirdByProjectList = ThirdByProject.getList(objApplicationCredentials, _
             id, _
             idproject, _
             idthird, _
             actions, _
             experiences, _
             type, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getThirdByProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ThirdByProject. ")

        Finally
            ' liberando recursos
            ThirdByProject = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ThirdByProject
    ''' </summary>
    ''' <param name="ThirdByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addThirdByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ThirdByProject As ThirdByProjectEntity) As Long

        ' definir los objetos
        Dim ThirdByProjectDALC As New ThirdByProjectDALC

        Try

            ' retornar el objeto
            addThirdByProject = ThirdByProjectDALC.add(objApplicationCredentials, ThirdByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addThirdByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ThirdByProject. ")

        Finally
            ' liberando recursos
            ThirdByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ThirdByProject por el Id
    ''' </summary>
    ''' <param name="idThirdByProject"></param>
    ''' <remarks></remarks>
    Public Function loadThirdByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThirdByProject As Integer) As ThirdByProjectEntity

        ' definir los objetos
        Dim ThirdByProjectDALC As New ThirdByProjectDALC

        Try

            ' retornar el objeto
            loadThirdByProject = ThirdByProjectDALC.load(objApplicationCredentials, idThirdByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadThirdByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ThirdByProject. ")

        Finally
            ' liberando recursos
            ThirdByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ThirdByProject
    ''' </summary>
    ''' <param name="ThirdByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateThirdByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ThirdByProject As ThirdByProjectEntity) As Long

        ' definir los objetos
        Dim ThirdByProjectDALC As New ThirdByProjectDALC

        Try

            ' retornar el objeto
            updateThirdByProject = ThirdByProjectDALC.update(objApplicationCredentials, ThirdByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateThirdByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ThirdByProject. ")

        Finally
            ' liberando recursos
            ThirdByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ThirdByProject de una forma
    ''' </summary>
    ''' <param name="idThirdByProject"></param>
    ''' <remarks></remarks>
    Public Sub deleteThirdByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThirdByProject As Integer, _
    Optional ByVal idProject As Integer = 0)

        ' definir los objetos
        Dim ThirdByProjectDALC As New ThirdByProjectDALC

        Try

            ' retornar el objeto
            ThirdByProjectDALC.delete(objApplicationCredentials, idThirdByProject, idProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteThirdByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ThirdByProject. ")

        Finally
            ' liberando recursos
            ThirdByProjectDALC = Nothing

        End Try

    End Sub

#End Region

#Region "OperatorByProject"

    ''' <summary>
    ''' Obtener la lista de OperatorByProject registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getOperatorByProjectList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal idoperator As String = "", _
        Optional ByVal order As String = "") As List(Of OperatorByProjectEntity)

        ' definir los objetos
        Dim OperatorByProject As New OperatorByProjectDALC

        Try

            ' retornar el objeto
            getOperatorByProjectList = OperatorByProject.getList(objApplicationCredentials, _
             id, _
             idproject, _
             idoperator, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getOperatorByProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de OperatorByProject. ")

        Finally
            ' liberando recursos
            OperatorByProject = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo OperatorByProject
    ''' </summary>
    ''' <param name="OperatorByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addOperatorByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal OperatorByProject As OperatorByProjectEntity) As Long

        ' definir los objetos
        Dim OperatorByProjectDALC As New OperatorByProjectDALC

        Try

            ' retornar el objeto
            addOperatorByProject = OperatorByProjectDALC.add(objApplicationCredentials, OperatorByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addOperatorByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un OperatorByProject. ")

        Finally
            ' liberando recursos
            OperatorByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un OperatorByProject por el Id
    ''' </summary>
    ''' <param name="idOperatorByProject"></param>
    ''' <remarks></remarks>
    Public Function loadOperatorByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idOperatorByProject As Integer) As OperatorByProjectEntity

        ' definir los objetos
        Dim OperatorByProjectDALC As New OperatorByProjectDALC

        Try

            ' retornar el objeto
            loadOperatorByProject = OperatorByProjectDALC.load(objApplicationCredentials, idOperatorByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadOperatorByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un OperatorByProject. ")

        Finally
            ' liberando recursos
            OperatorByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo OperatorByProject
    ''' </summary>
    ''' <param name="OperatorByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateOperatorByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal OperatorByProject As OperatorByProjectEntity) As Long

        ' definir los objetos
        Dim OperatorByProjectDALC As New OperatorByProjectDALC

        Try

            ' retornar el objeto
            updateOperatorByProject = OperatorByProjectDALC.update(objApplicationCredentials, OperatorByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateOperatorByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un OperatorByProject. ")

        Finally
            ' liberando recursos
            OperatorByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el OperatorByProject de una forma
    ''' </summary>
    ''' <param name="idOperatorByProject"></param>
    ''' <remarks></remarks>
    Public Sub deleteOperatorByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idOperatorByProject As Integer, _
    Optional ByVal idProject As Integer = 0)

        ' definir los objetos
        Dim OperatorByProjectDALC As New OperatorByProjectDALC

        Try

            ' retornar el objeto
            OperatorByProjectDALC.delete(objApplicationCredentials, idOperatorByProject, idProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteOperatorByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un OperatorByProject. ")

        Finally
            ' liberando recursos
            OperatorByProjectDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ProgramComponentByProject"

    ''' <summary>
    ''' Obtener la lista de ProgramComponentByProject registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProgramComponentByProjectList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal idProgramComponent As String = "", _
        Optional ByVal order As String = "") As List(Of ProgramComponentByProjectEntity)

        ' definir los objetos
        Dim ProgramComponentByProject As New ProgramComponentByProjectDALC

        Try

            ' retornar el objeto
            getProgramComponentByProjectList = ProgramComponentByProject.getList(objApplicationCredentials, _
             id, _
             idproject, _
             idProgramComponent, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProgramComponentByProjectList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProgramComponentByProject. ")

        Finally
            ' liberando recursos
            ProgramComponentByProject = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ProgramComponentByProject
    ''' </summary>
    ''' <param name="ProgramComponentByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addProgramComponentByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProgramComponentByProject As ProgramComponentByProjectEntity) As Long

        ' definir los objetos
        Dim ProgramComponentByProjectDALC As New ProgramComponentByProjectDALC

        Try

            ' retornar el objeto
            addProgramComponentByProject = ProgramComponentByProjectDALC.add(objApplicationCredentials, ProgramComponentByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addProgramComponentByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ProgramComponentByProject. ")

        Finally
            ' liberando recursos
            ProgramComponentByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProgramComponentByProject por el Id
    ''' </summary>
    ''' <param name="idProgramComponentByProject"></param>
    ''' <remarks></remarks>
    Public Function loadProgramComponentByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponentByProject As Integer) As ProgramComponentByProjectEntity

        ' definir los objetos
        Dim ProgramComponentByProjectDALC As New ProgramComponentByProjectDALC

        Try

            ' retornar el objeto
            loadProgramComponentByProject = ProgramComponentByProjectDALC.load(objApplicationCredentials, idProgramComponentByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadProgramComponentByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProgramComponentByProject. ")

        Finally
            ' liberando recursos
            ProgramComponentByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProgramComponentByProject
    ''' </summary>
    ''' <param name="ProgramComponentByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProgramComponentByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProgramComponentByProject As ProgramComponentByProjectEntity) As Long

        ' definir los objetos
        Dim ProgramComponentByProjectDALC As New ProgramComponentByProjectDALC

        Try

            ' retornar el objeto
            updateProgramComponentByProject = ProgramComponentByProjectDALC.update(objApplicationCredentials, ProgramComponentByProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProgramComponentByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ProgramComponentByProject. ")

        Finally
            ' liberando recursos
            ProgramComponentByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProgramComponentByProject de una forma
    ''' </summary>
    ''' <param name="idProgramComponentByProject"></param>
    ''' <remarks></remarks>
    Public Sub deleteProgramComponentByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponentByProject As Integer, _
    Optional ByVal idProject As Integer = 0)

        ' definir los objetos
        Dim ProgramComponentByProjectDALC As New ProgramComponentByProjectDALC

        Try

            ' retornar el objeto
            ProgramComponentByProjectDALC.delete(objApplicationCredentials, idProgramComponentByProject, idProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProgramComponentByProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ProgramComponentByProject. ")

        Finally
            ' liberando recursos
            ProgramComponentByProjectDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Forum"

    ''' <summary>
    ''' Obtener la lista de Forum registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getForumList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal subject As String = "", _
        Optional ByVal message As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal updateddate As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal replycount As String = "", _
        Optional ByVal lastreplyusername As String = "", _
        Optional ByVal lastreplycreatedate As String = "", _
        Optional ByVal order As String = "") As List(Of ForumEntity)

        ' definir los objetos
        Dim Forum As New ForumDALC

        Try

            ' retornar el objeto
            getForumList = Forum.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idproject, _
             projectname, _
             subject, _
             message, _
             attachment, _
             updateddate, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             replycount, _
             lastreplyusername, _
             lastreplycreatedate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getForumList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Forum. ")

        Finally
            ' liberando recursos
            Forum = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Forum
    ''' </summary>
    ''' <param name="Forum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addForum(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Forum As ForumEntity) As Long

        ' definir los objetos
        Dim ForumDALC As New ForumDALC

        Try

            ' retornar el objeto
            addForum = ForumDALC.add(objApplicationCredentials, Forum)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addForum")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Forum. ")

        Finally
            ' liberando recursos
            ForumDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Forum por el Id
    ''' </summary>
    ''' <param name="idForum"></param>
    ''' <remarks></remarks>
    Public Function loadForum(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idForum As Integer) As ForumEntity

        ' definir los objetos
        Dim ForumDALC As New ForumDALC

        Try

            ' retornar el objeto
            loadForum = ForumDALC.load(objApplicationCredentials, idForum)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadForum")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Forum. ")

        Finally
            ' liberando recursos
            ForumDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Forum
    ''' </summary>
    ''' <param name="Forum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateForum(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Forum As ForumEntity) As Long

        ' definir los objetos
        Dim ForumDALC As New ForumDALC

        Try

            ' retornar el objeto
            updateForum = ForumDALC.update(objApplicationCredentials, Forum)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateForum")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Forum. ")

        Finally
            ' liberando recursos
            ForumDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Forum de una forma
    ''' </summary>
    ''' <param name="idForum"></param>
    ''' <remarks></remarks>
    Public Sub deleteForum(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idForum As Integer)

        ' definir los objetos
        Dim ForumDALC As New ForumDALC
        Dim objReplyDALC As New ReplyDALC

        Try

            'Eliminar las respuestas asociadas
            objReplyDALC.delete(objApplicationCredentials, 0, idForum)

            ' retornar el objeto
            ForumDALC.delete(objApplicationCredentials, idForum)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteForum")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Forum. ")

        Finally
            ' liberando recursos
            ForumDALC = Nothing
            objReplyDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Risk"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyRiskCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Risk As New RiskDALC

        Try

            ' retornar el objeto
            verifyRiskCode = Risk.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            Risk = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Risk registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getRiskList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal whatcanhappen As String = "", _
        Optional ByVal riskimpact As String = "", _
        Optional ByVal ocurrenceprobability As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal idcomponent As String = "", _
        Optional ByVal componentname As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of RiskEntity)

        ' definir los objetos
        Dim Risk As New RiskDALC

        Try

            ' retornar el objeto
            getRiskList = Risk.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             description, _
             whatcanhappen, _
             riskimpact, _
             ocurrenceprobability, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             idcomponent, _
             componentname, _
             idproject, _
             order, _
             idKey, _
             isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getRiskList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Risk. ")

        Finally
            ' liberando recursos
            Risk = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Risk
    ''' </summary>
    ''' <param name="Risk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addRisk(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Risk As RiskEntity) As Long

        ' definir los objetos
        Dim RiskDALC As New RiskDALC
        Dim objComponentByRiskDALC As New ComponentByRiskDALC

        Try

            ' retornar el objeto
            addRisk = RiskDALC.add(objApplicationCredentials, Risk)

            'Guardar la lista de componentes
            For Each objComponentByRisk As ComponentByRiskEntity In Risk.componentlist
                objComponentByRisk.idrisk = addRisk
                objComponentByRiskDALC.add(objApplicationCredentials, objComponentByRisk)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addRisk")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Risk. ")

        Finally
            ' liberando recursos
            RiskDALC = Nothing
            objComponentByRiskDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Risk por el Id
    ''' </summary>
    ''' <param name="idRisk"></param>
    ''' <remarks></remarks>
    Public Function loadRisk(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idRisk As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As RiskEntity

        ' definir los objetos
        Dim RiskDALC As New RiskDALC

        Try

            ' retornar el objeto
            loadRisk = RiskDALC.load(objApplicationCredentials, idRisk, consultLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadRisk")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Risk. ")

        Finally
            ' liberando recursos
            RiskDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Risk
    ''' </summary>
    ''' <param name="Risk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateRisk(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Risk As RiskEntity) As Long

        ' definir los objetos
        Dim RiskDALC As New RiskDALC
        Dim objComponentByRiskDALC As New ComponentByRiskDALC
        Dim oldRisk As New RiskEntity
        Dim repeated As Boolean

        Try


            'cargar el objeto referenciado antes de la actualización
            oldRisk = RiskDALC.load(objApplicationCredentials, Risk.id)

            'Comparar la lista de fechas antigua con la nueva lista
            'Borra de la base de datos las que no estan en la lista nueva
            For Each objComponentByRisk As ComponentByRiskEntity In oldRisk.componentlist
                repeated = False
                For Each objComponentByRisk1 As ComponentByRiskEntity In Risk.componentlist
                    If objComponentByRisk.Equals(objComponentByRisk1) Then
                        repeated = True
                    End If
                Next
                If Not repeated Then
                    objComponentByRiskDALC.delete(objApplicationCredentials, objComponentByRisk.id)
                End If
            Next

            'Ingresa a la base de datos fechas de la nueva lista
            For Each objComponentByRisk As ComponentByRiskEntity In Risk.componentlist
                If objComponentByRisk.id = 0 Then
                    objComponentByRisk.idrisk = Risk.id
                    objComponentByRiskDALC.add(objApplicationCredentials, objComponentByRisk)
                End If
            Next

            ' actualizar y retornar el objeto
            updateRisk = RiskDALC.update(objApplicationCredentials, Risk)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateRisk")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Risk. ")

        Finally
            ' liberando recursos
            RiskDALC = Nothing
            objComponentByRiskDALC = Nothing
            oldRisk = Nothing
            repeated = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Risk de una forma
    ''' </summary>
    ''' <param name="idRisk"></param>
    ''' <remarks></remarks>
    Public Sub deleteRisk(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idRisk As Integer, _
        ByVal idKey As Integer)

        ' definir los objetos
        Dim RiskDALC As New RiskDALC
        Dim objComponentByRiskDALC As New ComponentByRiskDALC
        Dim objComponentByRiskList As New List(Of ComponentByRiskEntity)

        Try
            'Cargar la lista de componentes del riesgo referenciado
            objComponentByRiskList = RiskDALC.load(objApplicationCredentials, idRisk).componentlist
            For Each componentByRiskList As ComponentByRiskEntity In objComponentByRiskList
                'Elimina cada componente del Riesgo referenciado
                objComponentByRiskDALC.delete(objApplicationCredentials, componentByRiskList.id)
            Next

            ' retornar el objeto
            RiskDALC.delete(objApplicationCredentials, idRisk, idKey)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteRisk")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Risk. ")

        Finally
            ' liberando recursos
            RiskDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Reply"

    ''' <summary>
    ''' Obtener la lista de Reply registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getReplyList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
  Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idforum As String = "", _
        Optional ByVal forumsubject As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal subject As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal updatedate As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ReplyEntity)

        ' definir los objetos
        Dim Reply As New ReplyDALC

        Try

            ' retornar el objeto
            getReplyList = Reply.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idforum, _
             forumsubject, _
             iduser, _
             username, _
             subject, _
             attachment, _
             updatedate, _
             createdate, _
             order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getReplyList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Reply. ")

        Finally
            ' liberando recursos
            Reply = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Reply
    ''' </summary>
    ''' <param name="Reply"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addReply(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Reply As ReplyEntity) As Long

        ' definir los objetos
        Dim ReplyDALC As New ReplyDALC

        Try

            ' retornar el objeto
            addReply = ReplyDALC.add(objApplicationCredentials, Reply)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addReply")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Reply. ")

        Finally
            ' liberando recursos
            ReplyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Reply por el Id
    ''' </summary>
    ''' <param name="idReply"></param>
    ''' <remarks></remarks>
    Public Function loadReply(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idReply As Integer) As ReplyEntity

        ' definir los objetos
        Dim ReplyDALC As New ReplyDALC

        Try

            ' retornar el objeto
            loadReply = ReplyDALC.load(objApplicationCredentials, idReply)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReply")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Reply. ")

        Finally
            ' liberando recursos
            ReplyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Reply por el Id
    ''' </summary>
    ''' <param name="idForum"></param>
    ''' <remarks></remarks>
    Public Function loadLastReplyByForum(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idForum As Integer) As ReplyEntity

        ' definir los objetos
        Dim ReplyDALC As New ReplyDALC

        Try

            ' retornar el objeto
            loadLastReplyByForum = ReplyDALC.lastByForum(objApplicationCredentials, idForum)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadLastReplyByForum")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Reply por IdForum. ")

        Finally
            ' liberando recursos
            ReplyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Reply
    ''' </summary>
    ''' <param name="Reply"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateReply(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Reply As ReplyEntity) As Long

        ' definir los objetos
        Dim ReplyDALC As New ReplyDALC

        Try

            ' retornar el objeto
            updateReply = ReplyDALC.update(objApplicationCredentials, Reply)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateReply")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Reply. ")

        Finally
            ' liberando recursos
            ReplyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Reply de una forma
    ''' </summary>
    ''' <param name="idReply"></param>
    ''' <param name="idForum"></param>
    ''' <remarks></remarks>
    Public Sub deleteReply(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idReply As Integer, _
       Optional ByVal idForum As Integer = 0)

        ' definir los objetos
        Dim ReplyDALC As New ReplyDALC

        Try

            ' retornar el objeto
            ReplyDALC.delete(objApplicationCredentials, idReply, idForum)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteReply")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Reply. ")

        Finally
            ' liberando recursos
            ReplyDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ProgramComponent"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyProgramComponentCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim ProgramComponent As New ProgramComponentDALC

        Try

            ' retornar el objeto
            verifyProgramComponentCode = ProgramComponent.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            ProgramComponent = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de ProgramComponent registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProgramComponentList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal idProgram As String = "", _
        Optional ByVal Programname As String = "", _
        Optional ByVal idresponsible As String = "", _
        Optional ByVal responsiblename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ProgramComponentEntity)

        ' definir los objetos
        Dim ProgramComponent As New ProgramComponentDALC

        Try

            ' retornar el objeto
            getProgramComponentList = ProgramComponent.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             description, _
             idProgram, _
             Programname, _
             idresponsible, _
             responsiblename, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProgramComponentList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProgramComponent. ")

        Finally
            ' liberando recursos
            ProgramComponent = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ProgramComponent
    ''' </summary>
    ''' <param name="ProgramComponent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addProgramComponent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProgramComponent As ProgramComponentEntity) As Long

        ' definir los objetos
        Dim ProgramComponentDALC As New ProgramComponentDALC

        Try

            ' retornar el objeto
            addProgramComponent = ProgramComponentDALC.add(objApplicationCredentials, ProgramComponent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addProgramComponent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ProgramComponent. ")

        Finally
            ' liberando recursos
            ProgramComponentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProgramComponent por el Id
    ''' </summary>
    ''' <param name="idProgramComponent"></param>
    ''' <remarks></remarks>
    Public Function loadProgramComponent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponent As Integer) As ProgramComponentEntity

        ' definir los objetos
        Dim ProgramComponentDALC As New ProgramComponentDALC

        Try

            ' retornar el objeto
            loadProgramComponent = ProgramComponentDALC.load(objApplicationCredentials, idProgramComponent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadProgramComponent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProgramComponent. ")

        Finally
            ' liberando recursos
            ProgramComponentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProgramComponent
    ''' </summary>
    ''' <param name="ProgramComponent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProgramComponent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProgramComponent As ProgramComponentEntity) As Long

        ' definir los objetos
        Dim ProgramComponentDALC As New ProgramComponentDALC

        Try

            ' retornar el objeto
            updateProgramComponent = ProgramComponentDALC.update(objApplicationCredentials, ProgramComponent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProgramComponent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ProgramComponent. ")

        Finally
            ' liberando recursos
            ProgramComponentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProgramComponent de una forma
    ''' </summary>
    ''' <param name="idProgramComponent"></param>
    ''' <remarks></remarks>
    Public Sub deleteProgramComponent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponent As Integer)

        ' definir los objetos
        Dim ProgramComponentDALC As New ProgramComponentDALC

        Try

            ' retornar el objeto
            ProgramComponentDALC.delete(objApplicationCredentials, idProgramComponent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProgramComponent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ProgramComponent. ")

        Finally
            ' liberando recursos
            ProgramComponentDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Metodo creado por Jose Olmes Torres Abril 28 de 2010
    ''' Carga una lista de programa de Componentes segun una Linea Estrategica dado
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>   
    ''' <param name="idStrategicLine">identificador de la Linea Estrategica</param>   
    ''' <returns>un objeto de tipo List(Of ProgramComponentEntity)</returns>
    ''' <remarks></remarks>
    Public Function getProgramComponentListByStrategicLine(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal idStrategicLine As String) As List(Of ProgramComponentEntity)

        ' definir los objetos
        Dim ProgramComponent As New ProgramComponentDALC

        Try

            ' retornar el objeto
            getProgramComponentListByStrategicLine = ProgramComponent.getListByStrategicLine(objApplicationCredentials, idStrategicLine)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProgramComponentList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Componentes del Programa por Linea Estrategia. ")

        Finally
            ' liberando recursos
            ProgramComponent = Nothing

        End Try

    End Function

#End Region

#Region "StrategicActivity"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyStrategicActivityCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim StrategicActivity As New StrategicActivityDALC

        Try

            ' retornar el objeto
            verifyStrategicActivityCode = StrategicActivity.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            StrategicActivity = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de StrategicActivity registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getStrategicActivityList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal idstrategy As String = "", _
        Optional ByVal strategyname As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal estimatedvalue As String = "", _
        Optional ByVal idresponsible As String = "", _
        Optional ByVal responsiblename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of StrategicActivityEntity)

        ' definir los objetos
        Dim StrategicActivity As New StrategicActivityDALC

        Try

            ' retornar el objeto
            getStrategicActivityList = StrategicActivity.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             description, _
             idstrategy, _
             strategyname, _
             begindate, _
             enddate, _
             estimatedvalue, _
             idresponsible, _
             responsiblename, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getStrategicActivityList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de StrategicActivity. ")

        Finally
            ' liberando recursos
            StrategicActivity = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo StrategicActivity
    ''' </summary>
    ''' <param name="StrategicActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addStrategicActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal StrategicActivity As StrategicActivityEntity) As Long

        ' definir los objetos
        Dim StrategicActivityDALC As New StrategicActivityDALC

        Try

            ' retornar el objeto
            addStrategicActivity = StrategicActivityDALC.add(objApplicationCredentials, StrategicActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addStrategicActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un StrategicActivity. ")

        Finally
            ' liberando recursos
            StrategicActivityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un StrategicActivity por el Id
    ''' </summary>
    ''' <param name="idStrategicActivity"></param>
    ''' <remarks></remarks>
    Public Function loadStrategicActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategicActivity As Integer) As StrategicActivityEntity

        ' definir los objetos
        Dim StrategicActivityDALC As New StrategicActivityDALC

        Try

            ' retornar el objeto
            loadStrategicActivity = StrategicActivityDALC.load(objApplicationCredentials, idStrategicActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadStrategicActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un StrategicActivity. ")

        Finally
            ' liberando recursos
            StrategicActivityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo StrategicActivity
    ''' </summary>
    ''' <param name="StrategicActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateStrategicActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal StrategicActivity As StrategicActivityEntity) As Long

        ' definir los objetos
        Dim StrategicActivityDALC As New StrategicActivityDALC

        Try

            ' retornar el objeto
            updateStrategicActivity = StrategicActivityDALC.update(objApplicationCredentials, StrategicActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateStrategicActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un StrategicActivity. ")

        Finally
            ' liberando recursos
            StrategicActivityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el StrategicActivity de una forma
    ''' </summary>
    ''' <param name="idStrategicActivity"></param>
    ''' <remarks></remarks>
    Public Sub deleteStrategicActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategicActivity As Integer)

        ' definir los objetos
        Dim StrategicActivityDALC As New StrategicActivityDALC

        Try

            ' retornar el objeto
            StrategicActivityDALC.delete(objApplicationCredentials, idStrategicActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteStrategicActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un StrategicActivity. ")

        Finally
            ' liberando recursos
            StrategicActivityDALC = Nothing

        End Try

    End Sub

#End Region

#Region "StrategicObjective"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyStrategicObjectiveCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim StrategicObjective As New STRATEGICOBJECTIVEDALC

        Try

            ' retornar el objeto
            verifyStrategicObjectiveCode = StrategicObjective.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            StrategicObjective = Nothing

        End Try

    End Function


    ''' <summary> 
    ''' Registar un nuevo StrategicObjective
    ''' </summary>
    ''' <param name="StrategicObjective"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addStrategicObjective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal StrategicObjective As STRATEGICOBJECTIVEEntity) As Long

        ' definir los objetos
        Dim StrategicObjectiveDALC As New STRATEGICOBJECTIVEDALC

        Try

            ' retornar el objeto
            addStrategicObjective = StrategicObjectiveDALC.add(objApplicationCredentials, StrategicObjective)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addStrategicObjective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un StrategicObjective. ")

        Finally
            ' liberando recursos
            StrategicObjectiveDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo StrategicObjective
    ''' </summary>
    ''' <param name="StrategicObjective"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateStrategicObjective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal StrategicObjective As STRATEGICOBJECTIVEEntity) As Long

        ' definir los objetos
        Dim StrategicObjectiveDALC As New STRATEGICOBJECTIVEDALC

        Try

            ' retornar el objeto
            updateStrategicObjective = StrategicObjectiveDALC.update(objApplicationCredentials, StrategicObjective)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateStrategicObjective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un StrategicObjective. ")

        Finally
            ' liberando recursos
            StrategicObjectiveDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el StrategicObjective de una forma
    ''' </summary>
    ''' <param name="idStrategicObjective"></param>
    ''' <remarks></remarks>
    Public Sub deleteStrategicObjective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategicObjective As Integer)

        ' definir los objetos
        Dim StrategicObjectiveDALC As New STRATEGICOBJECTIVEDALC

        Try

            ' retornar el objeto
            StrategicObjectiveDALC.delete(objApplicationCredentials, idStrategicObjective)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteStrategicObjective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un StrategicObjective. ")

        Finally
            ' liberando recursos
            StrategicObjectiveDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Cargar un StrategicObjective por el Id
    ''' </summary>
    ''' <param name="idStrategicObjective"></param>
    ''' <remarks></remarks>
    Public Function loadStrategicObjective(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategicObjective As Integer) As STRATEGICOBJECTIVEEntity

        ' definir los objetos
        Dim StrategicObjectiveDALC As New STRATEGICOBJECTIVEDALC

        Try

            ' retornar el objeto
            loadStrategicObjective = StrategicObjectiveDALC.load(objApplicationCredentials, idStrategicObjective)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadStrategicObjective")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un StrategicObjective. ")

        Finally
            ' liberando recursos
            StrategicObjectiveDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de StrategicObjective registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getStrategicObjectiveList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal year As String = "", _
        Optional ByVal idperspective As String = "", _
        Optional ByVal perspectivename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of STRATEGICOBJECTIVEEntity)

        ' definir los objetos
        Dim StrategicObjective As New STRATEGICOBJECTIVEDALC

        Try

            ' retornar el objeto
            getStrategicObjectiveList = StrategicObjective.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             year, _
             idperspective, _
             perspectivename, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getStrategicObjectiveList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de StrategicObjective. ")

        Finally
            ' liberando recursos
            StrategicObjective = Nothing

        End Try

    End Function

#End Region

#Region "Strategy"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyStrategyCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Strategy As New STRATEGYDALC

        Try

            ' retornar el objeto
            verifyStrategyCode = Strategy.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            Strategy = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Strategy
    ''' </summary>
    ''' <param name="Strategy"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addStrategy(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Strategy As STRATEGYEntity) As Long

        ' definir los objetos
        Dim StrategyDALC As New STRATEGYDALC

        Try

            ' retornar el objeto
            addStrategy = StrategyDALC.add(objApplicationCredentials, Strategy)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addStrategy")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Strategy. ")

        Finally
            ' liberando recursos
            StrategyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Strategy
    ''' </summary>
    ''' <param name="Strategy"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateStrategy(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Strategy As STRATEGYEntity) As Long

        ' definir los objetos
        Dim StrategyDALC As New STRATEGYDALC

        Try

            ' retornar el objeto
            updateStrategy = StrategyDALC.update(objApplicationCredentials, Strategy)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateStrategy")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Strategy. ")

        Finally
            ' liberando recursos
            StrategyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Strategy de una forma
    ''' </summary>
    ''' <param name="idStrategy"></param>
    ''' <remarks></remarks>
    Public Sub deleteStrategy(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategy As Integer)

        ' definir los objetos
        Dim StrategyDALC As New STRATEGYDALC

        Try

            ' retornar el objeto
            StrategyDALC.delete(objApplicationCredentials, idStrategy)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteStrategy")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Strategy. ")

        Finally
            ' liberando recursos
            StrategyDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Cargar un Strategy por el Id
    ''' </summary>
    ''' <param name="idStrategy"></param>
    ''' <remarks></remarks>
    Public Function loadStrategy(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategy As Integer) As STRATEGYEntity

        ' definir los objetos
        Dim StrategyDALC As New STRATEGYDALC

        Try

            ' retornar el objeto
            loadStrategy = StrategyDALC.load(objApplicationCredentials, idStrategy)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadStrategy")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Strategy. ")

        Finally
            ' liberando recursos
            StrategyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Strategy registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getStrategyList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idstrategicobjective As String = "", _
        Optional ByVal strategicobjectivename As String = "", _
        Optional ByVal idmanagment As String = "", _
        Optional ByVal managmentname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of STRATEGYEntity)

        ' definir los objetos
        Dim Strategy As New STRATEGYDALC

        Try

            ' retornar el objeto
            getStrategyList = Strategy.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             idstrategicobjective, _
             strategicobjectivename, _
             idmanagment, _
             managmentname, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getStrategyList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Strategy. ")

        Finally
            ' liberando recursos
            Strategy = Nothing

        End Try

    End Function

#End Region

#Region "SubActivity"


    ''' <summary> 
    ''' Verifica si ya existe el número o código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifySubActivityNumber(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal number As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim SubActivity As New SubActivityDALC

        Try

            ' retornar el objeto
            verifySubActivityNumber = SubActivity.verifyNumber(objApplicationCredentials, number, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código o número de SubActivity. ")

        Finally
            ' liberando recursos
            SubActivity = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de SubActivity registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSubActivityList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idactivity As String = "", _
        Optional ByVal activitytitle As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal typetext As String = "", _
        Optional ByVal number As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal idresponsible As String = "", _
        Optional ByVal responsiblename As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal totalcost As String = "", _
        Optional ByVal duration As String = "", _
        Optional ByVal fsccontribution As String = "", _
        Optional ByVal ofcontribution As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal criticalpath As String = "", _
        Optional ByVal requiresapproval As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal componentname As String = "", _
        Optional ByVal objectivename As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of SubActivityEntity)

        ' definir los objetos
        Dim SubActivity As New SubActivityDALC

        Try

            ' retornar el objeto
            getSubActivityList = SubActivity.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idactivity, _
             activitytitle, _
             type, _
             typetext, _
             number, _
             name, _
             description, _
             idresponsible, _
             responsiblename, _
             begindate, _
             enddate, _
             totalcost, _
             duration, _
             fsccontribution, _
             ofcontribution, _
             attachment, _
             criticalpath, _
             requiresapproval, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             componentname, _
             objectivename, _
             projectname, _
             order, _
             idKey, _
             isLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getSubActivityList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de SubActivity. ")

        Finally
            ' liberando recursos
            SubActivity = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo SubActivity
    ''' </summary>
    ''' <param name="SubActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addSubActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal SubActivity As SubActivityEntity) As Long

        ' definir los objetos
        Dim SubActivityDALC As New SubActivityDALC

        Try

            ' retornar el objeto
            addSubActivity = SubActivityDALC.add(objApplicationCredentials, SubActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addSubActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un SubActivity. ")

        Finally
            ' liberando recursos
            SubActivityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un SubActivity por el Id
    ''' </summary>
    ''' <param name="idSubActivity"></param>
    ''' <remarks></remarks>
    Public Function loadSubActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSubActivity As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As SubActivityEntity

        ' definir los objetos
        Dim SubActivityDALC As New SubActivityDALC

        Try

            ' retornar el objeto
            loadSubActivity = SubActivityDALC.load(objApplicationCredentials, idSubActivity, consultLastVersion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadSubActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un SubActivity. ")

        Finally
            ' liberando recursos
            SubActivityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo SubActivity
    ''' </summary>
    ''' <param name="SubActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateSubActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal SubActivity As SubActivityEntity) As Long

        ' definir los objetos
        Dim SubActivityDALC As New SubActivityDALC

        Try

            ' retornar el objeto
            updateSubActivity = SubActivityDALC.update(objApplicationCredentials, SubActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateSubActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un SubActivity. ")

        Finally
            ' liberando recursos
            SubActivityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el SubActivity de una forma
    ''' </summary>
    ''' <param name="idSubActivity"></param>
    ''' <remarks></remarks>
    Public Sub deleteSubActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSubActivity As Integer, _
        ByVal idKey As Integer)

        ' definir los objetos
        Dim SubActivityDALC As New SubActivityDALC

        Try

            ' retornar el objeto
            SubActivityDALC.delete(objApplicationCredentials, idSubActivity, idKey)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteSubActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un SubActivity. ")

        Finally
            ' liberando recursos
            SubActivityDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Typecontract"

    ''' <summary>
    ''' Obtener la lista de Third registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' TODO: 20 modificacion por nuevos combo tipo de contrato
    ''' Autor: german Rodiguez 20/07/2013
    Public Function gettypecontract(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
     Optional ByVal id As String = "", _
     Optional ByVal contract As String = "", _
     Optional ByVal order As String = "") As List(Of typecontractEntity)

        ' definir los objetos
        Dim typecontract As New typecontractDALC

        Try

            ' retornar el objeto
            gettypecontract = typecontract.getList(objApplicationCredentials, _
            id, _
            contract, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "gettypecontractList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de typecontract. ")

        Finally
            ' liberando recursos
            typecontract = Nothing

        End Try

    End Function

    ' TODO: 20 modificacion por nuevos combo tipo de contrato
    ' Autor: german Rodiguez 20/07/2013
    ' cierre de cambio
#End Region

#Region "ProjectApprovalDefinitive"
    ''' <summary>
    ''' TODO: Funcion que trae la lista de proyectos aprobados
    ''' Autor: Pedro Cruz
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProjectListcontract(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                                     Optional ByVal id As Integer = 0, _
                                                     Optional ByVal code As String = "" _
                                                     ) As List(Of ProjectEntity)

        Dim Project = New ProjectDALC

        Try

            getProjectListcontract = Project.getListcontract(objApplicationCredentials, id, code)


            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getThirdList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Proyectos.")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try

    End Function

    Public Function finishproject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idproject As Integer) As String

        Dim Project = New ProjectDALC

        Try
            Project.finalUpdate(objApplicationCredentials, idproject)
            Return "ok"
        Catch ex As Exception
            Return "no"
        End Try

    End Function

#End Region

#Region "Third"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyThirdCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Third As New ThirdDALC

        Try

            ' retornar el objeto
            verifyThirdCode = Third.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. ")

        Finally
            ' liberando recursos
            Third = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Obtener la lista de Third registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' TODO:38 modificacion por nuevos campos en terceros
    ''' Autor: German Rodiguez 27/06/2013 MGgroup
    Public Function getThirdList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal contact As String = "", _
        Optional ByVal document As String = "", _
        Optional ByVal phone As String = "", _
        Optional ByVal email As String = "", _
        Optional ByVal actions As String = "", _
        Optional ByVal experiences As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ThirdEntity)

        ' TODO:38 modificacion por nuevos campos en terceros
        ' Autor: German Rodiguez 27/06/2013 MGgroup
        ' cierre de cambios

        ' definir los objetos
        Dim Third As New ThirdDALC

        Try

            ' retornar el objeto
            getThirdList = Third.getList(objApplicationCredentials, _
            id, _
            idlike, _
            code, _
            name, _
            contact, _
            document, _
            phone, _
            email, _
            actions, _
            experiences, _
            enabled, _
            enabledtext, _
            iduser, _
            username, _
            createdate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getThirdList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Third. ")

        Finally
            ' liberando recursos
            Third = Nothing

        End Try

    End Function

    Public Function getThirdList2(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      Optional ByVal id As String = "", _
      Optional ByVal idlike As String = "", _
      Optional ByVal code As String = "", _
      Optional ByVal name As String = "", _
      Optional ByVal enabled As String = "", _
      Optional ByVal enabledtext As String = "", _
      Optional ByVal iduser As String = "", _
      Optional ByVal username As String = "", _
      Optional ByVal createdate As String = "", _
      Optional ByVal order As String = "") As List(Of ThirdEntity)

        ' definir los objetos
        Dim Third As New ThirdDALC

        Try

            ' retornar el objeto
            getThirdList2 = Third.getList2(objApplicationCredentials, _
            id, _
            idlike, _
            code, _
            name, _
            enabled, _
            enabledtext, _
            iduser, _
            username, _
            createdate, _
            order)


            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getThirdList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Third. ")

        Finally
            ' liberando recursos
            Third = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Third
    ''' </summary>
    ''' <param name="Third"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addThird(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Third As ThirdEntity) As Long

        ' definir los objetos
        Dim ThirdDALC As New ThirdDALC

        Try

            ' retornar el objeto
            addThird = ThirdDALC.add(objApplicationCredentials, Third)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addThird")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Third. ")

        Finally
            ' liberando recursos
            ThirdDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Third por el Id
    ''' </summary>
    ''' <param name="idThird"></param>
    ''' <remarks></remarks>
    Public Function loadThird(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThird As Integer) As ThirdEntity

        ' definir los objetos
        Dim ThirdDALC As New ThirdDALC

        Try

            ' retornar el objeto
            loadThird = ThirdDALC.load(objApplicationCredentials, idThird)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadThird")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Third. ")

        Finally
            ' liberando recursos
            ThirdDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Third
    ''' </summary>
    ''' <param name="Third"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateThird(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Third As ThirdEntity) As Long

        ' definir los objetos
        Dim ThirdDALC As New ThirdDALC

        Try

            ' retornar el objeto
            updateThird = ThirdDALC.update(objApplicationCredentials, Third)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateThird")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Third. ")

        Finally
            ' liberando recursos
            ThirdDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Third de una forma
    ''' </summary>
    ''' <param name="idThird"></param>
    ''' <remarks></remarks>
    Public Sub deleteThird(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThird As Integer)

        ' definir los objetos
        Dim ThirdDALC As New ThirdDALC

        Try

            ' retornar el objeto
            ThirdDALC.delete(objApplicationCredentials, idThird)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteThird")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Third. ")

        Finally
            ' liberando recursos
            ThirdDALC = Nothing

        End Try

    End Sub

    Public Function getNaturalThird(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As List(Of ThirdEntity)

        Dim Third As New ThirdDALC

        Try
            getNaturalThird = Third.getList(objApplicationCredentials, , , , , , , , , , , , , , , 1)
        Catch ex As Exception
            'cancelar el proceso
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getNaturalThird")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al cargar Supervisores. ")

        Finally
            'liberar recursos
            Third = Nothing

        End Try

    End Function

#End Region

#Region "User"

    ''' <summary>
    ''' Cargar lista de cargar los Usuarios.
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getUserList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable

        ' definiendo los objetos
        Dim User As New UserDALC

        Try
            ' ejecutar la intruccion
            getUserList = User.getList(objApplicationCredentials)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getUserList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los usuarios.")

        Finally

            ' liberando recursos
            User = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar el nomrbe del usuario
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getUserName(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                            ByVal id As Integer) As String

        ' definiendo los objetos
        Dim User As New UserDALC

        Try
            ' ejecutar la intruccion
            getUserName = User.getName(objApplicationCredentials, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getUserName")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el nomber del usuario.")

        Finally

            ' liberando recursos
            User = Nothing

        End Try

    End Function

#End Region

#Region "Idea"

    ''' <summary>
    ''' Verifica que la entidad no este aprovada
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="EntryData"></param>
    ''' <param name="idEntryData"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyapprove(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal EntryData As String, _
     ByVal idEntryData As String, _
        ByVal Status As String) As Boolean
        ' definir los objetos
        Dim Idea As New IdeaDALC

        Try

            ' retornar el objeto
            verifyapprove = Idea.verifyapprove(objApplicationCredentials, EntryData, idEntryData, Status)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyapprove")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar si la entidad esta aprobada. ")

        Finally
            ' liberando recursos
            Idea = Nothing

        End Try
    End Function


    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyIdeaCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Idea As New IdeaDALC

        Try

            ' retornar el objeto
            verifyIdeaCode = Idea.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de Formato archivo anexo. ")

        Finally
            ' liberando recursos
            Idea = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Idea registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' TODO: 28 facade getidealist se crean nuevos campos
    ''' Autor: German Rodriguez MGgroup
    ''' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

    Public Function getIdeaList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal objective As String = "", _
        Optional ByVal startdate As String = "", _
        Optional ByVal duration As String = "", _
        Optional ByVal areadescription As String = "", _
        Optional ByVal population As String = "", _
        Optional ByVal cost As String = "", _
        Optional ByVal strategydescription As String = "", _
        Optional ByVal results As String = "", _
        Optional ByVal source As String = "", _
        Optional ByVal idsummoning As String = "", _
        Optional ByVal startprocess As String = "", _
        Optional ByVal startprocesstext As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal StrategicLineName As String = "", _
        Optional ByVal ProgramName As String = "", _
        Optional ByVal ProgramComponentName As String = "", _
        Optional ByVal Loadingobservations As String = "", _
        Optional ByVal ResultsKnowledgeManagement As String = "", _
        Optional ByVal ResultsInstalledCapacity As String = "", _
        Optional ByVal Contacts As String = "", _
        Optional ByVal Vrmoney As String = "", _
        Optional ByVal VrSpecies As String = "", _
        Optional ByVal FSCorCounterpartContribution As String = "", _
        Optional ByVal order As String = "") As List(Of IdeaEntity)

        ' TODO: 28 facade getidealist se crean nuevos campos
        ' Autor: German Rodriguez MGgroup
        ' cierre de cambios

        ' definir los objetos
        Dim Idea As New IdeaDALC

        Try

            ' retornar el objeto
            getIdeaList = Idea.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             objective, _
             startdate, _
             duration, _
             areadescription, _
             population, _
             cost, _
             strategydescription, _
             results, _
             source, _
             idsummoning, _
             startprocess, _
             startprocesstext, _
             createdate, _
             iduser, _
             username, _
             enabled, _
             enabledtext, _
             StrategicLineName, _
             ProgramName, _
             ProgramComponentName, _
             Loadingobservations, _
             ResultsKnowledgeManagement, _
             ResultsInstalledCapacity, _
             Contacts, _
             order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getIdeaList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Idea. ")

        Finally
            ' liberando recursos
            Idea = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de Idea registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' TODO: facade getidealistApproved
    ''' Autor: Hernan Gomez MG Group
    ''' decripciòn: se crean consulta de ideas aprobadas para poder crear proyecto FSC fase II

    Public Function getIdeaListApproved(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal objective As String = "", _
        Optional ByVal startdate As String = "", _
        Optional ByVal duration As String = "", _
        Optional ByVal areadescription As String = "", _
        Optional ByVal population As String = "", _
        Optional ByVal cost As String = "", _
        Optional ByVal strategydescription As String = "", _
        Optional ByVal results As String = "", _
        Optional ByVal source As String = "", _
        Optional ByVal idsummoning As String = "", _
        Optional ByVal startprocess As String = "", _
        Optional ByVal startprocesstext As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal StrategicLineName As String = "", _
        Optional ByVal ProgramName As String = "", _
        Optional ByVal ProgramComponentName As String = "", _
        Optional ByVal Loadingobservations As String = "", _
        Optional ByVal ResultsKnowledgeManagement As String = "", _
        Optional ByVal ResultsInstalledCapacity As String = "", _
        Optional ByVal Contacts As String = "", _
        Optional ByVal Vrmoney As String = "", _
        Optional ByVal VrSpecies As String = "", _
        Optional ByVal FSCorCounterpartContribution As String = "", _
        Optional ByVal order As String = "") As List(Of IdeaEntity)

        ' definir los objetos
        Dim Project As New ProjectDALC

        Try

            ' retornar el objeto
            getIdeaListApproved = Project.getListIdeaAprobada(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getIdeaList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Idea. ")

        Finally
            ' liberando recursos
            Project = Nothing

        End Try

    End Function



    ''' <summary> 
    ''' Registar un nuevo Idea
    ''' </summary>
    ''' <param name="Idea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Idea As IdeaEntity) As Long

        'Definir los objetos
        Dim IdeaDALC As New IdeaDALC
        Dim objPaymentFlowDALC As New PaymentFlowDALC()
        Dim objdetallesflujoDALC As New DetailedcashflowsDALC()
        Dim miIdIdea As Long

        Try

            'Se llama al metodo que almacena la información general de la Idea
            miIdIdea = IdeaDALC.add(objApplicationCredentials, Idea)

            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (Idea.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In Idea.DOCUMENTLIST
                    'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                    If (document.attachfile.Length > 0) Then

                        'Se instancia un objeto de tipo documento por entidad
                        Dim documentByEntity As New DocumentsByEntityEntity()

                        'Se almacena el documento y se recupera su Id
                        documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                        documentByEntity.idnentity = miIdIdea
                        documentByEntity.entityname = Idea.GetType.ToString()

                        'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                        Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        document.ISNEW = False
                    End If
                Next
            End If

            'Se recorre la lista de ubicaciones por idea
            For Each locationByIdea As LocationByIdeaEntity In Idea.LOCATIONBYIDEALIST
                'Por cada elemento de la lista
                'Se agrega el id de la idea 
                locationByIdea.ididea = miIdIdea
                'Se llama al metodo que almacena la informacion de las ubicaciones por idea.
                Me.addLocationByIdea(objApplicationCredentials, locationByIdea)
            Next

            'Se recorre la lista de terceros por idea
            For Each thirdByIdea As ThirdByIdeaEntity In Idea.THIRDBYIDEALIST
                'Por cada elemento de la lista
                'Se agrega el id de la idea 
                thirdByIdea.ididea = miIdIdea
                'Se llama al metodo que almacena la informacion de los terceros por idea.
                Me.addThirdByIdea(objApplicationCredentials, thirdByIdea)
            Next

            'Se recorre la lista de Componentes del Programa por idea
            For Each ProgramComponentByIdea As ProgramComponentByIdeaEntity In Idea.ProgramComponentBYIDEALIST
                'Por cada elemento de la lista
                'Se agrega el id de la idea 
                ProgramComponentByIdea.ididea = miIdIdea
                'Se llama al metodo que almacena la informacion de las Componentes del Programa por idea.
                Me.addProgramComponentByIdea(objApplicationCredentials, ProgramComponentByIdea)
            Next

            For Each objPaymentFlow As PaymentFlowEntity In Idea.paymentflowByProjectList
                objPaymentFlow.ididea = miIdIdea
                objPaymentFlowDALC.add(objApplicationCredentials, objPaymentFlow)
            Next

            For Each objdeteallesflujos As DetailedcashflowsEntity In Idea.DetailedcashflowsbyIdeaList
                objdeteallesflujos.IdIdea = miIdIdea
                objdetallesflujoDALC.add(objApplicationCredentials, objdeteallesflujos)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

            Return miIdIdea

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar una Idea. ")

        Finally
            ' liberando recursos
            IdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Idea por el Id
    ''' </summary>
    ''' <param name="idIdea"></param>
    ''' <remarks></remarks>
    Public Function loadIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer) As IdeaEntity

        ' definir los objetos
        Dim IdeaDALC As New IdeaDALC

        Try
            ' retornar el objeto
            loadIdea = IdeaDALC.load(objApplicationCredentials, idIdea)

            'Se carga la lista de documentos para el registro de idea actual
            loadIdea.DOCUMENTSBYIDEALIST = Me.getDocumentsByEntityList(objApplicationCredentials, idnentity:=idIdea, entityName:=GetType(IdeaEntity).ToString())

            'Se verifica que existam documentos anexos al registro actual
            If (Not loadIdea.DOCUMENTSBYIDEALIST Is Nothing AndAlso loadIdea.DOCUMENTSBYIDEALIST.Count > 0) Then
                'Se recorre la lista de identificadores de documentos agregados
                Dim idsDocuments As String = ""
                For Each documentByEntity As DocumentsByEntityEntity In loadIdea.DOCUMENTSBYIDEALIST
                    idsDocuments &= documentByEntity.iddocuments.ToString() & ","
                Next
                If (idsDocuments.Length > 0) Then idsDocuments = idsDocuments.Substring(0, idsDocuments.Length - 1)

                'Se carga la lista de documentos requeridos
                loadIdea.DOCUMENTLIST = Me.getDocumentsListByEntity(objApplicationCredentials, idsDocuments)
            End If

            'Se llama al metodo que pemite cargar la lista ubicaciones por idea
            '  loadIdea.LOCATIONBYIDEALIST = getLocationByIdeaList(objApplicationCredentials, , loadIdea.id, , , )

            'Se llama al metodo que pemite cargar la lista terceros por idea
            'loadIdea.THIRDBYIDEALIST = getThirdByIdeaList(objApplicationCredentials, , loadIdea.id, , , , , )

            'Se llama al metodo que pemite cargar la lista Componentes del Programa por idea
            loadIdea.ProgramComponentBYIDEALIST = getProgramComponentByIdeaList(objApplicationCredentials, , loadIdea.id, , )

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Idea. ")

        Finally
            ' liberando recursos
            IdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Idea
    ''' </summary>
    ''' <param name="Idea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Idea As IdeaEntity) As Long

        ' definir los objetos
        Dim IdeaDALC As New IdeaDALC
        Dim idthird As Integer

        Dim objPaymentFlowDALC As New PaymentFlowDALC()
        Dim objdetallesflujoDALC As New DetailedcashflowsDALC()

        Try

            'Se modfica la informacion principal de la idea
            updateIdea = IdeaDALC.update(objApplicationCredentials, Idea)

            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (Idea.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In Idea.DOCUMENTLIST

                    'Se verifica si el archivo actual es un archivo nuevo
                    If (document.ISNEW) Then

                        'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                        If (document.attachfile.Length > 0) Then

                            'Se instancia un objeto de tipo documento por entidad
                            Dim documentByEntity As New DocumentsByEntityEntity()

                            'Se almacena el documento y se recupera su Id
                            documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                            documentByEntity.idnentity = Idea.id
                            documentByEntity.entityname = Idea.GetType.ToString()

                            'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                            Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        End If

                        'Se verifica si el archivo ha sido borrado
                    ElseIf (document.ISDELETED) Then

                        'Se llama al metodo que permite eliminar el registro de documentos por entidad
                        Me.deleteAllDocumentsByDocumentAndEntity(objApplicationCredentials, document.id, Idea.id, Idea.GetType.ToString())

                        'Se llama al metodo que permite eliminar el documento requerido
                        Me.deleteDocuments(objApplicationCredentials, document.id, document.attachfile)

                    End If
                Next
            End If

            'Se elimina la informacion existente de las ubicaciones para la idea actual
            Me.deleteAllLocationByIdea(objApplicationCredentials, Idea.id)
            'Se recorre la lista de ubicaciones por idea
            For Each locationByIdea As LocationByIdeaEntity In Idea.LOCATIONBYIDEALIST
                'Por cada elemento de la lista
                'Se agrega el id de la idea 
                locationByIdea.ididea = Idea.id
                'Se llama al metodo que almacena la informacion de las ubicaciones por idea.
                Me.addLocationByIdea(objApplicationCredentials, locationByIdea)
            Next

            'Se elimina la informacion existente de los terceros para la idea actual
            Me.deleteAllThirdByIdea(objApplicationCredentials, Idea.id)
            'Se recorre la lista de terceros por idea
            For Each thirdByIdea As ThirdByIdeaEntity In Idea.THIRDBYIDEALIST
                'Por cada elemento de la lista
                'Se agrega el id de la idea 
                thirdByIdea.ididea = Idea.id
                'Se llama al metodo que almacena la informacion de los terceros por idea.
                Me.addThirdByIdea(objApplicationCredentials, thirdByIdea)
            Next


            'Se elimina la informacion existente de las Componentes del Programa para la idea actual
            Me.deleteAllProgramComponentByIdea(objApplicationCredentials, Idea.id)
            'Se recorre la lista de Componentes del Programa por idea
            For Each ProgramComponentByIdea As ProgramComponentByIdeaEntity In Idea.ProgramComponentBYIDEALIST
                'Por cada elemento de la lista
                'Se agrega el id de la idea 
                ProgramComponentByIdea.ididea = Idea.id
                'Se llama al metodo que almacena la informacion de las Componentes del Programa por idea.
                Me.addProgramComponentByIdea(objApplicationCredentials, ProgramComponentByIdea)
            Next


            'Se elimina la informacion existente de los flujos de pago para la idea actual
            objPaymentFlowDALC.delete(objApplicationCredentials, Idea.id)
            'Guardar la lista de flujos de pago de la idea
            For Each objPaymentFlow As PaymentFlowEntity In Idea.paymentflowByProjectList
                objPaymentFlow.ididea = Idea.id
                objPaymentFlowDALC.add(objApplicationCredentials, objPaymentFlow)
            Next

            'Se elimina la informacion existente de los detalles de flujo para la idea actual
            objdetallesflujoDALC.delete(objApplicationCredentials, Idea.id)

            'Guardar la lista de detalles flujos de pago de la idea
            For Each objdeteallesflujos As DetailedcashflowsEntity In Idea.DetailedcashflowsbyIdeaList
                objdeteallesflujos.IdIdea = Idea.id
                objdetallesflujoDALC.add(objApplicationCredentials, objdeteallesflujos)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Idea. ")

        Finally
            ' liberando recursos
            IdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Idea de una forma
    ''' </summary>
    ''' <param name="idIdea"></param>
    ''' <remarks></remarks>
    Public Sub deleteIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer, ByVal documentsList As List(Of DocumentsEntity))

        ' definir los objetos
        Dim IdeaDALC As New IdeaDALC

        Try

            If (Not documentsList Is Nothing) Then
                'Se recorre la lista fisica de documentos adjuntos a la idea actual
                For Each document As DocumentsEntity In documentsList

                    'Se elimina la lista de documentos adjuntos
                    Me.deleteDocuments(objApplicationCredentials, document.id, document.attachfile)

                Next
            End If

            'Se elimina en la B.D. la lista de documentos por entidad
            Me.deleteAllDocumentsByEntity(objApplicationCredentials, idIdea, GetType(IdeaEntity).ToString())

            'Se elimina la informacion existente de las ubicaciones para la idea actual
            Me.deleteAllLocationByIdea(objApplicationCredentials, idIdea)

            'Se elimina la informacion existente de los terceros para la idea actual
            Me.deleteAllThirdByIdea(objApplicationCredentials, idIdea)

            'Se elimina la informacion existente de las Componentes del Programa para la idea actual
            Me.deleteAllProgramComponentByIdea(objApplicationCredentials, idIdea)

            ' Se elimina la idea actual
            IdeaDALC.delete(objApplicationCredentials, idIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'Se verifica si la excepción interna generada es de tipo SqlClient.SqlException
            If Not ex.InnerException Is Nothing AndAlso ex.InnerException.GetType.ToString() = "System.Data.SqlClient.SqlException" Then

                Dim myException As SqlClient.SqlException = ex.InnerException

                'Se verifica si el error es por integridad referencial
                If myException.Number = 547 Then

                    ' cancelar la transaccion
                    CtxSetAbort()

                    'publicar el error
                    GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteIdea")
                    ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                    ' subir el error de nivel
                    Throw New Exception("Ha ocurrido un error al intentar eliminar este registro debido a una relación existente con un registro de Proyecto.")

                End If

            Else

                ' cancelar la transaccion
                CtxSetAbort()

                ' publicar el error
                GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteIdea")
                ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                ' subir el error de nivel
                Throw New Exception("Error al eliminar un Idea. ")

            End If

        Finally
            ' liberando recursos
            IdeaDALC = Nothing

        End Try

    End Sub

#End Region

#Region "DocumentsByEntity"

    ''' <summary>
    ''' Obtener la lista de DocumentsByEntity registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDocumentsByEntityList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal iddocuments As String = "", _
        Optional ByVal idnentity As String = "", _
        Optional ByVal entityName As String = "", _
        Optional ByVal order As String = "") As List(Of DocumentsByEntityEntity)

        ' definir los objetos
        Dim DocumentsByEntity As New DocumentsByEntityDALC

        Try

            ' retornar el objeto
            getDocumentsByEntityList = DocumentsByEntity.getList(objApplicationCredentials, _
             id, _
             iddocuments, _
             idnentity, _
             entityName, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getDocumentsByEntityList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de DocumentsByEntity. ")

        Finally
            ' liberando recursos
            DocumentsByEntity = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo DocumentsByEntity
    ''' </summary>
    ''' <param name="DocumentsByEntity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addDocumentsByEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal DocumentsByEntity As DocumentsByEntityEntity) As Long

        ' definir los objetos
        Dim DocumentsByEntityDALC As New DocumentsByEntityDALC

        Try

            ' retornar el objeto
            addDocumentsByEntity = DocumentsByEntityDALC.add(objApplicationCredentials, DocumentsByEntity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addDocumentsByEntity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un DocumentsByEntity. ")

        Finally
            ' liberando recursos
            DocumentsByEntityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un DocumentsByEntity por el Id
    ''' </summary>
    ''' <param name="idDocumentsByEntity"></param>
    ''' <remarks></remarks>
    Public Function loadDocumentsByEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocumentsByEntity As Integer) As DocumentsByEntityEntity

        ' definir los objetos
        Dim DocumentsByEntityDALC As New DocumentsByEntityDALC

        Try

            ' retornar el objeto
            loadDocumentsByEntity = DocumentsByEntityDALC.load(objApplicationCredentials, idDocumentsByEntity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadDocumentsByEntity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un DocumentsByEntity. ")

        Finally
            ' liberando recursos
            DocumentsByEntityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo DocumentsByEntity
    ''' </summary>
    ''' <param name="DocumentsByEntity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateDocumentsByEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal DocumentsByEntity As DocumentsByEntityEntity) As Long

        ' definir los objetos
        Dim DocumentsByEntityDALC As New DocumentsByEntityDALC

        Try

            ' retornar el objeto
            updateDocumentsByEntity = DocumentsByEntityDALC.update(objApplicationCredentials, DocumentsByEntity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateDocumentsByEntity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un DocumentsByEntity. ")

        Finally
            ' liberando recursos
            DocumentsByEntityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el DocumentsByEntity de una forma
    ''' </summary>
    ''' <param name="idDocumentsByEntity"></param>
    ''' <remarks></remarks>
    Public Sub deleteDocumentsByEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocumentsByEntity As Integer)

        ' definir los objetos
        Dim DocumentsByEntityDALC As New DocumentsByEntityDALC

        Try

            ' retornar el objeto
            DocumentsByEntityDALC.delete(objApplicationCredentials, idDocumentsByEntity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteDocumentsByEntity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un DocumentsByEntity. ")

        Finally
            ' liberando recursos
            DocumentsByEntityDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra el registro asociado a una entidad determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idEntity">identificador de la entidad</param>
    ''' <param name="nameEntity">nombre de la entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteAllDocumentsByEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idEntity As Integer, ByVal entityName As String) As Long

        ' definir los objetos
        Dim documentsByEntityDALC As New DocumentsByEntityDALC

        Try

            ' retornar el objeto
            documentsByEntityDALC.deleteByEntity(objApplicationCredentials, idEntity, entityName)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteDocuments")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un documento por entidad. ")

        Finally
            ' liberando recursos
            documentsByEntityDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el registro asociado a un documento y entidad determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idDocument">identificador del documento</param>
    ''' <param name="idEntity">identificador de la entidad</param>
    ''' <param name="nameEntity">nombre de la entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteAllDocumentsByDocumentAndEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocument As Integer, ByVal idEntity As Integer, ByVal entityName As String) As Long

        ' definir los objetos
        Dim documentsByEntityDALC As New DocumentsByEntityDALC

        Try

            ' retornar el objeto
            documentsByEntityDALC.deleteByDocumentAndEntity(objApplicationCredentials, idDocument, idEntity, entityName)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteDocuments")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un documento por entidad. ")

        Finally
            ' liberando recursos
            documentsByEntityDALC = Nothing

        End Try

    End Function

#End Region

#Region "LocationByIdea"

    ''' <summary>
    ''' Obtener la lista de LocationByIdea registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getLocationByIdeaList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal ididea As String = "", _
        Optional ByVal iddepto As String = "", _
        Optional ByVal idcity As String = "", _
        Optional ByVal order As String = "") As List(Of LocationByIdeaEntity)

        ' definir los objetos
        Dim LocationByIdea As New LocationByIdeaDALC

        Try

            ' retornar el objeto
            getLocationByIdeaList = LocationByIdea.getList(objApplicationCredentials, _
             id, _
             ididea, _
             iddepto, _
             idcity, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getLocationByIdeaList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de LocationByIdea. ")

        Finally
            ' liberando recursos
            LocationByIdea = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo LocationByIdea
    ''' </summary>
    ''' <param name="LocationByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addLocationByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal LocationByIdea As LocationByIdeaEntity) As Long

        ' definir los objetos
        Dim LocationByIdeaDALC As New LocationByIdeaDALC

        Try

            ' retornar el objeto
            addLocationByIdea = LocationByIdeaDALC.add(objApplicationCredentials, LocationByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addLocationByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un LocationByIdea. ")

        Finally
            ' liberando recursos
            LocationByIdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un LocationByIdea por el Id
    ''' </summary>
    ''' <param name="idLocationByIdea"></param>
    ''' <remarks></remarks>
    Public Function loadLocationByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idLocationByIdea As Integer) As LocationByIdeaEntity

        ' definir los objetos
        Dim LocationByIdeaDALC As New LocationByIdeaDALC

        Try

            ' retornar el objeto
            loadLocationByIdea = LocationByIdeaDALC.load(objApplicationCredentials, idLocationByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadLocationByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un LocationByIdea. ")

        Finally
            ' liberando recursos
            LocationByIdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo LocationByIdea
    ''' </summary>
    ''' <param name="LocationByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateLocationByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal LocationByIdea As LocationByIdeaEntity) As Long

        ' definir los objetos
        Dim LocationByIdeaDALC As New LocationByIdeaDALC

        Try

            ' retornar el objeto
            updateLocationByIdea = LocationByIdeaDALC.update(objApplicationCredentials, LocationByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateLocationByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un LocationByIdea. ")

        Finally
            ' liberando recursos
            LocationByIdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el LocationByIdea de una forma
    ''' </summary>
    ''' <param name="idLocationByIdea"></param>
    ''' <remarks></remarks>
    Public Sub deleteLocationByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idLocationByIdea As Integer)

        ' definir los objetos
        Dim LocationByIdeaDALC As New LocationByIdeaDALC

        Try

            ' retornar el objeto
            LocationByIdeaDALC.delete(objApplicationCredentials, idLocationByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteLocationByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un LocationByIdea. ")

        Finally
            ' liberando recursos
            LocationByIdeaDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra todas las ubicaciones por idea de una Idea
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idIdea">identificador de la idea</param>
    ''' <remarks></remarks>
    Public Sub deleteAllLocationByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer)

        ' definir los objetos
        Dim LocationByIdeaDALC As New LocationByIdeaDALC

        Try

            ' retornar el objeto
            LocationByIdeaDALC.deleteAll(objApplicationCredentials, idIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteLocationByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar la lista de ubicaciones por idea. ")

        Finally
            ' liberando recursos
            LocationByIdeaDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ThirdByIdea"

    ''' <summary>
    ''' Obtener la lista de ThirdByIdea registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getThirdByIdeaList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal ididea As String = "", _
        Optional ByVal idthird As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal vrmoney As String = "", _
        Optional ByVal vrspecies As String = "", _
        Optional ByVal FSCorCounterpartContribution As String = "", _
        Optional ByVal order As String = "") As List(Of ThirdByIdeaEntity)

        ' definir los objetos
        Dim ThirdByIdea As New ThirdByIdeaDALC

        Try

            ' retornar el objeto
            getThirdByIdeaList = ThirdByIdea.getList(objApplicationCredentials, _
            id, _
            ididea, _
            idthird, _
            type, _
            vrmoney, _
            vrspecies, _
            FSCorCounterpartContribution, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getThirdByIdeaList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ThirdByIdea. ")

        Finally
            ' liberando recursos
            ThirdByIdea = Nothing

        End Try

    End Function


    ''' <summary> 
    ''' Registar un nuevo ThirdByIdea
    ''' </summary>
    ''' <param name="ThirdByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addThirdByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ThirdByIdea As ThirdByIdeaEntity) As Long

        ' definir los objetos
        Dim ThirdByIdeaDALC As New ThirdByIdeaDALC

        Try

            ' retornar el objeto
            addThirdByIdea = ThirdByIdeaDALC.add(objApplicationCredentials, ThirdByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addThirdByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ThirdByIdea. ")

        Finally
            ' liberando recursos
            ThirdByIdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ThirdByIdea por el Id
    ''' </summary>
    ''' <param name="idThirdByIdea"></param>
    ''' <remarks></remarks>
    Public Function loadThirdByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThirdByIdea As Integer) As ThirdByIdeaEntity

        ' definir los objetos
        Dim ThirdByIdeaDALC As New ThirdByIdeaDALC

        Try

            ' retornar el objeto
            loadThirdByIdea = ThirdByIdeaDALC.load(objApplicationCredentials, idThirdByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadThirdByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ThirdByIdea. ")

        Finally
            ' liberando recursos
            ThirdByIdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ThirdByIdea
    ''' </summary>
    ''' <param name="ThirdByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateThirdByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ThirdByIdea As ThirdByIdeaEntity) As Long

        ' definir los objetos
        Dim ThirdByIdeaDALC As New ThirdByIdeaDALC

        Try

            ' retornar el objeto
            updateThirdByIdea = ThirdByIdeaDALC.update(objApplicationCredentials, ThirdByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateThirdByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ThirdByIdea. ")

        Finally
            ' liberando recursos
            ThirdByIdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ThirdByIdea de una forma
    ''' </summary>
    ''' <param name="idThirdByIdea"></param>
    ''' <remarks></remarks>
    Public Sub deleteThirdByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThirdByIdea As Integer)

        ' definir los objetos
        Dim ThirdByIdeaDALC As New ThirdByIdeaDALC

        Try

            ' retornar el objeto
            ThirdByIdeaDALC.delete(objApplicationCredentials, idThirdByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteThirdByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ThirdByIdea. ")

        Finally
            ' liberando recursos
            ThirdByIdeaDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra los terceros por idea de una diea determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idIdea">identificador de la idea</param>
    ''' <remarks></remarks>
    Public Sub deleteAllThirdByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer)

        ' definir los objetos
        Dim ThirdByIdeaDALC As New ThirdByIdeaDALC

        Try

            ' retornar el objeto
            ThirdByIdeaDALC.deleteAll(objApplicationCredentials, idIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteThirdByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ThirdByIdea. ")

        Finally
            ' liberando recursos
            ThirdByIdeaDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ProgramComponentByIdea"

    ''' <summary>
    ''' Obtener la lista de ProgramComponentByIdea registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProgramComponentByIdeaList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal ididea As String = "", _
        Optional ByVal idProgramComponent As String = "", _
        Optional ByVal order As String = "") As List(Of ProgramComponentByIdeaEntity)

        ' definir los objetos
        Dim ProgramComponentByIdea As New ProgramComponentByIdeaDALC

        Try

            ' retornar el objeto
            getProgramComponentByIdeaList = ProgramComponentByIdea.getList(objApplicationCredentials, _
             id, _
             ididea, _
             idProgramComponent, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProgramComponentByIdeaList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            ProgramComponentByIdea = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ProgramComponentByIdea
    ''' </summary>
    ''' <param name="ProgramComponentByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addProgramComponentByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProgramComponentByIdea As ProgramComponentByIdeaEntity) As Long

        ' definir los objetos
        Dim ProgramComponentByIdeaDALC As New ProgramComponentByIdeaDALC

        Try

            ' retornar el objeto
            addProgramComponentByIdea = ProgramComponentByIdeaDALC.add(objApplicationCredentials, ProgramComponentByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addProgramComponentByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            ProgramComponentByIdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProgramComponentByIdea por el Id
    ''' </summary>
    ''' <param name="idProgramComponentByIdea"></param>
    ''' <remarks></remarks>
    Public Function loadProgramComponentByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponentByIdea As Integer) As ProgramComponentByIdeaEntity

        ' definir los objetos
        Dim ProgramComponentByIdeaDALC As New ProgramComponentByIdeaDALC

        Try

            ' retornar el objeto
            loadProgramComponentByIdea = ProgramComponentByIdeaDALC.load(objApplicationCredentials, idProgramComponentByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadProgramComponentByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            ProgramComponentByIdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProgramComponentByIdea
    ''' </summary>
    ''' <param name="ProgramComponentByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProgramComponentByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProgramComponentByIdea As ProgramComponentByIdeaEntity) As Long

        ' definir los objetos
        Dim ProgramComponentByIdeaDALC As New ProgramComponentByIdeaDALC

        Try

            ' retornar el objeto
            updateProgramComponentByIdea = ProgramComponentByIdeaDALC.update(objApplicationCredentials, ProgramComponentByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProgramComponentByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            ProgramComponentByIdeaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProgramComponentByIdea de una forma
    ''' </summary>
    ''' <param name="idProgramComponentByIdea"></param>
    ''' <remarks></remarks>
    Public Sub deleteProgramComponentByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponentByIdea As Integer)

        ' definir los objetos
        Dim ProgramComponentByIdeaDALC As New ProgramComponentByIdeaDALC

        Try

            ' retornar el objeto
            ProgramComponentByIdeaDALC.delete(objApplicationCredentials, idProgramComponentByIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProgramComponentByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            ProgramComponentByIdeaDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra la informacion de las Componentes del Programa de una idea determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idIdea">identificador de la idea</param>
    ''' <remarks></remarks>
    Public Sub deleteAllProgramComponentByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer)

        ' definir los objetos
        Dim ProgramComponentByIdeaDALC As New ProgramComponentByIdeaDALC

        Try
            ' retornar el objeto
            ProgramComponentByIdeaDALC.deleteAll(objApplicationCredentials, idIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProgramComponentByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            ProgramComponentByIdeaDALC = Nothing

        End Try

    End Sub

#End Region

#Region "AttachFileFormat"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyAttachFileFormatCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim AttachFileFormat As New AttachFileFormatDALC

        Try

            ' retornar el objeto
            verifyAttachFileFormatCode = AttachFileFormat.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de Formato archivo anexo. ")

        Finally
            ' liberando recursos
            AttachFileFormat = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de AttachFileFormat registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getAttachFileFormatList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal order As String = "") As List(Of AttachFileFormatEntity)

        ' definir los objetos
        Dim AttachFileFormat As New AttachFileFormatDALC

        Try

            ' retornar el objeto
            getAttachFileFormatList = AttachFileFormat.getList(objApplicationCredentials, _
            id, _
            idlike, _
            code, _
            name, _
            createdate, _
            iduser, _
            username, _
            enabled, _
            enabledtext, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getAttachFileFormatList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de AttachFileFormat. ")

        Finally
            ' liberando recursos
            AttachFileFormat = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo AttachFileFormat
    ''' </summary>
    ''' <param name="AttachFileFormat"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addAttachFileFormat(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal AttachFileFormat As AttachFileFormatEntity) As Long

        ' definir los objetos
        Dim AttachFileFormatDALC As New AttachFileFormatDALC

        Try

            ' retornar el objeto
            addAttachFileFormat = AttachFileFormatDALC.add(objApplicationCredentials, AttachFileFormat)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addAttachFileFormat")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un AttachFileFormat. ")

        Finally
            ' liberando recursos
            AttachFileFormatDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un AttachFileFormat por el Id
    ''' </summary>
    ''' <param name="idAttachFileFormat"></param>
    ''' <remarks></remarks>
    Public Function loadAttachFileFormat(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAttachFileFormat As Integer) As AttachFileFormatEntity

        ' definir los objetos
        Dim AttachFileFormatDALC As New AttachFileFormatDALC

        Try

            ' retornar el objeto
            loadAttachFileFormat = AttachFileFormatDALC.load(objApplicationCredentials, idAttachFileFormat)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadAttachFileFormat")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un AttachFileFormat. ")

        Finally
            ' liberando recursos
            AttachFileFormatDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo AttachFileFormat
    ''' </summary>
    ''' <param name="AttachFileFormat"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateAttachFileFormat(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal AttachFileFormat As AttachFileFormatEntity) As Long

        ' definir los objetos
        Dim AttachFileFormatDALC As New AttachFileFormatDALC

        Try

            ' retornar el objeto
            updateAttachFileFormat = AttachFileFormatDALC.update(objApplicationCredentials, AttachFileFormat)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateAttachFileFormat")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un AttachFileFormat. ")

        Finally
            ' liberando recursos
            AttachFileFormatDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el AttachFileFormat de una forma
    ''' </summary>
    ''' <param name="idAttachFileFormat"></param>
    ''' <remarks></remarks>
    Public Sub deleteAttachFileFormat(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAttachFileFormat As Integer)

        ' definir los objetos
        Dim AttachFileFormatDALC As New AttachFileFormatDALC

        Try

            ' retornar el objeto
            AttachFileFormatDALC.delete(objApplicationCredentials, idAttachFileFormat)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteAttachFileFormat")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un AttachFileFormat. ")

        Finally
            ' liberando recursos
            AttachFileFormatDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Documents"

    ''' <summary>
    ''' Obtener la lista de Documents registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDocumentsList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal title As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal ideditedfor As String = "", _
        Optional ByVal editedforname As String = "", _
        Optional ByVal idvisibilitylevel As String = "", _
        Optional ByVal visibilitylevelname As String = "", _
        Optional ByVal iddocumenttype As String = "", _
        Optional ByVal documenttypename As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal attachfile As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal entityName As String = "", _
         Optional ByVal ProjectName As String = "", _
        Optional ByVal order As String = "") As List(Of DocumentsEntity)

        ' definir los objetos
        Dim Documents As New DocumentsDALC

        Try

            ' retornar el objeto
            getDocumentsList = Documents.getList(objApplicationCredentials, _
             id, _
             idlike, _
             title, _
             description, _
             ideditedfor, _
             editedforname, _
             idvisibilitylevel, _
             visibilitylevelname, _
             iddocumenttype, _
             documenttypename, _
             createdate, _
             iduser, _
             username, _
             attachfile, _
             enabled, _
             enabledtext, _
             entityName, _
            ProjectName, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getDocumentsList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Documents. ")

        Finally
            ' liberando recursos
            Documents = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Documents
    ''' </summary>
    ''' <param name="Documents"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addDocuments(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Documents As DocumentsEntity) As Long

        ' definir los objetos
        Dim DocumentsDALC As New DocumentsDALC

        Try

            ' retornar el objeto
            addDocuments = DocumentsDALC.add(objApplicationCredentials, Documents)

            'Se agrega el registro en la tabla documentos por entidad
            'Dim documentByEntity As New DocumentsByEntityEntity()
            'documentByEntity.iddocuments = addDocuments
            'documentByEntity.idnentity = addDocuments
            'documentByEntity.entityname = Documents.GetType.ToString()
            ''Se llama al metodo que permite almacenar la información del objeto documento por entidad
            'Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addDocuments")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Documents. ")

        Finally
            ' liberando recursos
            DocumentsDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Documents por el Id
    ''' </summary>
    ''' <param name="idDocuments"></param>
    ''' <remarks></remarks>
    Public Function loadDocuments(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocuments As Integer) As DocumentsEntity

        ' definir los objetos
        Dim DocumentsDALC As New DocumentsDALC

        Try

            ' retornar el objeto
            loadDocuments = DocumentsDALC.load(objApplicationCredentials, idDocuments)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadDocuments")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Documents. ")

        Finally
            ' liberando recursos
            DocumentsDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Documents
    ''' </summary>
    ''' <param name="Documents"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateDocuments(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Documents As DocumentsEntity) As Long

        ' definir los objetos
        Dim DocumentsDALC As New DocumentsDALC

        Try
            'Se actualiza la informacion del registro
            updateDocuments = DocumentsDALC.update(objApplicationCredentials, Documents)

            'Se verifica si el archivo anexo ha sido modificado
            If (Documents.ISMODIFIED) Then
                'Se extrae la ruta del archivo nateriormente almacenado
                Dim miRutaArchivo As String = PublicFunction.getSettingValue("documentPath") & "/" & Documents.ATTACHFILEOLD
                miRutaArchivo = HttpContext.Current.Server.MapPath(miRutaArchivo)
                Dim miFileInfo As New FileInfo(miRutaArchivo)
                'Se verifica si el archivo anterior existe
                If (miFileInfo.Exists) Then
                    'Se elimina el archivo anterior
                    File.Delete(miRutaArchivo)
                End If
            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateDocuments")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Documents. ")

        Finally
            ' liberando recursos
            DocumentsDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Documents de una forma
    ''' </summary>
    ''' <param name="idDocuments"></param>
    ''' <remarks></remarks>
    Public Sub deleteDocuments(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocuments As Integer, ByVal nameAttachFile As String)

        ' definir los objetos
        Dim DocumentsDALC As New DocumentsDALC
        Dim document As New DocumentsEntity()

        Try

            'Se llama al metodo que permite eliminar el registro de documentos por entidad
            Me.deleteAllDocumentsByDocumentAndEntity(objApplicationCredentials, idDocuments, idDocuments, document.GetType.ToString())

            ' retornar el objeto
            DocumentsDALC.delete(objApplicationCredentials, idDocuments)

            'Se leimina el archivo adjunto al documento actual
            Dim miRutaArchivo As String = PublicFunction.getSettingValue("documentPath") & "/" & nameAttachFile
            miRutaArchivo = HttpContext.Current.Server.MapPath(miRutaArchivo)
            Dim miFileInfo As New FileInfo(miRutaArchivo)
            'Se verifica si el archivo anterior existe
            If (miFileInfo.Exists) Then
                'Se elimina el archivo anterior
                File.Delete(miRutaArchivo)
            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteDocuments")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Documents. ")

        Finally
            ' liberando recursos
            DocumentsDALC = Nothing
            document = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Permite consultar una lista de documentos anexos a una entidad determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idsDocuments">una lista separa por comas de los ids de los documentos requeridos</param>
    ''' <returns>una lista de documentos</returns>
    ''' <remarks></remarks>
    Public Function getDocumentsListByEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         Optional ByVal idsDocuments As String = "") As List(Of DocumentsEntity)

        ' definir los objetos
        Dim Documents As New DocumentsDALC

        Try

            'Retornar el objeto
            getDocumentsListByEntity = Documents.getListByEntity(objApplicationCredentials, idsDocuments)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getDocumentsList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Documentos. ")

        Finally
            ' liberando recursos
            Documents = Nothing

        End Try

    End Function

#End Region

#Region "SupplierEvaluation"

    ''' <summary>
    ''' Obtener la lista de SupplierEvaluation registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSupplierEvaluationList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idsupplier As String = "", _
        Optional ByVal suppliername As String = "", _
        Optional ByVal contractnumber As String = "", _
        Optional ByVal contractstartdate As String = "", _
        Optional ByVal contractenddate As String = "", _
        Optional ByVal contractsubject As String = "", _
        Optional ByVal contractvalue As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of SupplierEvaluationEntity)

        ' definir los objetos
        Dim SupplierEvaluation As New SupplierEvaluationDALC

        Try

            ' retornar el objeto
            getSupplierEvaluationList = SupplierEvaluation.getList(objApplicationCredentials, _
             id, _
             idsupplier, _
             suppliername, _
             contractnumber, _
             contractstartdate, _
             contractenddate, _
             contractsubject, _
             contractvalue, _
             iduser, _
             username, _
             createdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getSupplierEvaluationList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de SupplierEvaluation. ")

        Finally
            ' liberando recursos
            SupplierEvaluation = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo SupplierEvaluation
    ''' </summary>
    ''' <param name="SupplierEvaluation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addSupplierEvaluation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal SupplierEvaluation As SupplierEvaluationEntity) As Long

        ' definir los objetos
        Dim SupplierEvaluationDALC As New SupplierEvaluationDALC

        Try
            Dim miIdProveeddor As Long = 0

            ' retornar el objeto
            miIdProveeddor = SupplierEvaluationDALC.add(objApplicationCredentials, SupplierEvaluation)

            'Se asigna el id de la evaluacion del proveedor actual
            SupplierEvaluation.SUPPLIERQUALIFICATION.idsupplierevaluation = miIdProveeddor

            'Se llama al metodo que permite insertar la calificación del proveedor
            Me.addSupplierQualification(objApplicationCredentials, SupplierEvaluation.SUPPLIERQUALIFICATION)

            ' finalizar la transaccion
            CtxSetComplete()

            Return miIdProveeddor

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addSupplierEvaluation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un SupplierEvaluation. ")

        Finally
            ' liberando recursos
            SupplierEvaluationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un SupplierEvaluation por el Id
    ''' </summary>
    ''' <param name="idSupplierEvaluation"></param>
    ''' <remarks></remarks>
    Public Function loadSupplierEvaluation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSupplierEvaluation As Integer) As SupplierEvaluationEntity

        ' definir los objetos
        Dim SupplierEvaluationDALC As New SupplierEvaluationDALC
        Dim MySupplierEvaluation As SupplierEvaluationEntity

        Try

            ' retornar el objeto
            MySupplierEvaluation = SupplierEvaluationDALC.load(objApplicationCredentials, idSupplierEvaluation)
            'Se carga lainformación de la calificacion del proveedor actual
            MySupplierEvaluation.SUPPLIERQUALIFICATION = Me.loadSupplierQualification(objApplicationCredentials, MySupplierEvaluation.id)

            ' finalizar la transaccion
            CtxSetComplete()

            Return MySupplierEvaluation

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadSupplierEvaluation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un SupplierEvaluation. ")

        Finally
            ' liberando recursos
            SupplierEvaluationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo SupplierEvaluation
    ''' </summary>
    ''' <param name="SupplierEvaluation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateSupplierEvaluation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal SupplierEvaluation As SupplierEvaluationEntity) As Long

        ' definir los objetos
        Dim SupplierEvaluationDALC As New SupplierEvaluationDALC

        Try

            ' retornar el objeto
            updateSupplierEvaluation = SupplierEvaluationDALC.update(objApplicationCredentials, SupplierEvaluation)

            'Se llama al metodo que permite actualizar la información de la calificacion del proveedor actual
            Me.updateSupplierQualification(objApplicationCredentials, SupplierEvaluation.SUPPLIERQUALIFICATION)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateSupplierEvaluation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un SupplierEvaluation. ")

        Finally
            ' liberando recursos
            SupplierEvaluationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el SupplierEvaluation de una forma
    ''' </summary>
    ''' <param name="idSupplierEvaluation"></param>
    ''' <remarks></remarks>
    Public Sub deleteSupplierEvaluation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSupplierEvaluation As Integer)

        ' definir los objetos
        Dim SupplierEvaluationDALC As New SupplierEvaluationDALC

        Try
            'Se llama al metodo que oermite eliminar la calificacion del proveedor actual
            Me.deleteSupplierQualification(objApplicationCredentials, idSupplierEvaluation)

            ' retornar el objeto
            SupplierEvaluationDALC.delete(objApplicationCredentials, idSupplierEvaluation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteSupplierEvaluation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un SupplierEvaluation. ")

        Finally
            ' liberando recursos
            SupplierEvaluationDALC = Nothing

        End Try

    End Sub

#End Region

#Region "SupplierQualification"

    ''' <summary>
    ''' Obtener la lista de SupplierQualification registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSupplierQualificationList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idsupplierevaluation As String = "", _
        Optional ByVal contractsubject As String = "", _
        Optional ByVal contractualobligations As String = "", _
        Optional ByVal definedgoals As String = "", _
        Optional ByVal agreeddeadlines As String = "", _
        Optional ByVal totalitydeliveredproducts As String = "", _
        Optional ByVal requestsmadebyfsc As String = "", _
        Optional ByVal deliveryproductsservices As String = "", _
        Optional ByVal reporting As String = "", _
        Optional ByVal productquality As String = "", _
        Optional ByVal reportsquality As String = "", _
        Optional ByVal accompanimentquality As String = "", _
        Optional ByVal attentioncomplaintsclaims As String = "", _
        Optional ByVal returnedproducts As String = "", _
        Optional ByVal productvalueadded As String = "", _
        Optional ByVal accompanimentvalueadded As String = "", _
        Optional ByVal reportsvalueadded As String = "", _
        Optional ByVal projectplaneacion As String = "", _
        Optional ByVal methodologyimplemented As String = "", _
        Optional ByVal developmentprojectorganization As String = "", _
        Optional ByVal jointactivities As String = "", _
        Optional ByVal projectcontrol As String = "", _
        Optional ByVal servicestaffcompetence As String = "", _
        Optional ByVal suppliercompetence As String = "", _
        Optional ByVal informationconfidentiality As String = "", _
        Optional ByVal compliancepercentage As String = "", _
        Optional ByVal opportunitypercentage As String = "", _
        Optional ByVal qualitypercentage As String = "", _
        Optional ByVal addedvaluepercentage As String = "", _
        Optional ByVal methodologypercentage As String = "", _
        Optional ByVal servicestaffcompetencepercentage As String = "", _
        Optional ByVal confidentialitypercentage As String = "", _
        Optional ByVal order As String = "") As List(Of SupplierQualificationEntity)

        ' definir los objetos
        Dim SupplierQualification As New SupplierQualificationDALC

        Try

            ' retornar el objeto
            getSupplierQualificationList = SupplierQualification.getList(objApplicationCredentials, _
             id, _
             idsupplierevaluation, _
             contractsubject, _
             contractualobligations, _
             definedgoals, _
             agreeddeadlines, _
             totalitydeliveredproducts, _
             requestsmadebyfsc, _
             deliveryproductsservices, _
             reporting, _
             productquality, _
             reportsquality, _
             accompanimentquality, _
             attentioncomplaintsclaims, _
             returnedproducts, _
             productvalueadded, _
             accompanimentvalueadded, _
             reportsvalueadded, _
             projectplaneacion, _
             methodologyimplemented, _
             developmentprojectorganization, _
             jointactivities, _
             projectcontrol, _
             servicestaffcompetence, _
             suppliercompetence, _
             informationconfidentiality, _
             compliancepercentage, _
             opportunitypercentage, _
             qualitypercentage, _
             addedvaluepercentage, _
             methodologypercentage, _
             servicestaffcompetencepercentage, _
             confidentialitypercentage, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getSupplierQualificationList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de SupplierQualification. ")

        Finally
            ' liberando recursos
            SupplierQualification = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo SupplierQualification
    ''' </summary>
    ''' <param name="SupplierQualification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addSupplierQualification(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal SupplierQualification As SupplierQualificationEntity) As Long

        ' definir los objetos
        Dim SupplierQualificationDALC As New SupplierQualificationDALC

        Try

            ' retornar el objeto
            addSupplierQualification = SupplierQualificationDALC.add(objApplicationCredentials, SupplierQualification)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addSupplierQualification")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un SupplierQualification. ")

        Finally
            ' liberando recursos
            SupplierQualificationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un SupplierQualification por el Id
    ''' </summary>
    ''' <param name="idSupplierQualification"></param>
    ''' <remarks></remarks>
    Public Function loadSupplierQualification(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSupplierEvaluation As Integer) As SupplierQualificationEntity

        ' definir los objetos
        Dim SupplierQualificationDALC As New SupplierQualificationDALC

        Try

            ' retornar el objeto
            loadSupplierQualification = SupplierQualificationDALC.load(objApplicationCredentials, idSupplierEvaluation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadSupplierQualification")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un SupplierQualification. ")

        Finally
            ' liberando recursos
            SupplierQualificationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo SupplierQualification
    ''' </summary>
    ''' <param name="SupplierQualification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateSupplierQualification(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal SupplierQualification As SupplierQualificationEntity) As Long

        ' definir los objetos
        Dim SupplierQualificationDALC As New SupplierQualificationDALC

        Try

            ' retornar el objeto
            updateSupplierQualification = SupplierQualificationDALC.update(objApplicationCredentials, SupplierQualification)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateSupplierQualification")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un SupplierQualification. ")

        Finally
            ' liberando recursos
            SupplierQualificationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el SupplierQualification de una forma
    ''' </summary>
    ''' <param name="idSupplierQualification"></param>
    ''' <remarks></remarks>
    Public Sub deleteSupplierQualification(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSupplierEvaluation As Integer)

        ' definir los objetos
        Dim SupplierQualificationDALC As New SupplierQualificationDALC

        Try

            ' retornar el objeto
            SupplierQualificationDALC.delete(objApplicationCredentials, idSupplierEvaluation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteSupplierQualification")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un SupplierQualification. ")

        Finally
            ' liberando recursos
            SupplierQualificationDALC = Nothing

        End Try

    End Sub

#End Region

#Region "DocumentType"

    ''' <summary>
    ''' Obtener la lista de DocumentType registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDocumentTypeList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of DocumentTypeEntity)

        ' definir los objetos
        Dim DocumentType As New DocumentTypeDALC

        Try

            ' retornar el objeto
            getDocumentTypeList = DocumentType.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getDocumentTypeList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Tipos de documento. ")

        Finally
            ' liberando recursos
            DocumentType = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo DocumentType
    ''' </summary>
    ''' <param name="DocumentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addDocumentType(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal DocumentType As DocumentTypeEntity) As Long

        ' definir los objetos
        Dim DocumentTypeDALC As New DocumentTypeDALC

        Try

            ' retornar el objeto
            addDocumentType = DocumentTypeDALC.add(objApplicationCredentials, DocumentType)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addDocumentType")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Tipo de documento. ")

        Finally
            ' liberando recursos
            DocumentTypeDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un DocumentType por el Id
    ''' </summary>
    ''' <param name="idDocumentType"></param>
    ''' <remarks></remarks>
    Public Function loadDocumentType(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocumentType As Integer) As DocumentTypeEntity

        ' definir los objetos
        Dim DocumentTypeDALC As New DocumentTypeDALC

        Try

            ' retornar el objeto
            loadDocumentType = DocumentTypeDALC.load(objApplicationCredentials, idDocumentType)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadDocumentType")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Tipo de documento. ")

        Finally
            ' liberando recursos
            DocumentTypeDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo DocumentType
    ''' </summary>
    ''' <param name="DocumentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateDocumentType(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal DocumentType As DocumentTypeEntity) As Long

        ' definir los objetos
        Dim DocumentTypeDALC As New DocumentTypeDALC

        Try

            ' retornar el objeto
            updateDocumentType = DocumentTypeDALC.update(objApplicationCredentials, DocumentType)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateDocumentType")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Tipo de documento. ")

        Finally
            ' liberando recursos
            DocumentTypeDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el DocumentType de una forma
    ''' </summary>
    ''' <param name="idDocumentType"></param>
    ''' <remarks></remarks>
    Public Sub deleteDocumentType(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocumentType As Integer)

        ' definir los objetos
        Dim DocumentTypeDALC As New DocumentTypeDALC

        Try

            ' retornar el objeto
            DocumentTypeDALC.delete(objApplicationCredentials, idDocumentType)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteDocumentType")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Tipo de documento. ")

        Finally
            ' liberando recursos
            DocumentTypeDALC = Nothing

        End Try

    End Sub

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code">codigo a verificar</param>
    ''' <returns>Verdadero si existe algún registro con el mismo código, falso en caso contrario</returns>
    ''' <remarks></remarks>
    Public Function verifyDocumentTypeCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim DocumentTypeDALC As New DocumentTypeDALC

        Try

            ' retornar el objeto
            verifyDocumentTypeCode = DocumentTypeDALC.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de Tipo de documento. ")

        Finally
            ' liberando recursos
            DocumentTypeDALC = Nothing

        End Try

    End Function

#End Region

#Region "VisibilityLevel"

    ''' <summary>
    ''' Obtener la lista de VisibilityLevel registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getVisibilityLevelList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of VisibilityLevelEntity)

        ' definir los objetos
        Dim VisibilityLevel As New VisibilityLevelDALC

        Try

            ' retornar el objeto
            getVisibilityLevelList = VisibilityLevel.getList(objApplicationCredentials, _
             id, _
             code, _
             name, _
             iduser, _
             enabled, _
             createdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getVisibilityLevelList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de VisibilityLevel. ")

        Finally
            ' liberando recursos
            VisibilityLevel = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo VisibilityLevel
    ''' </summary>
    ''' <param name="VisibilityLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addVisibilityLevel(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal VisibilityLevel As VisibilityLevelEntity) As Long

        ' definir los objetos
        Dim VisibilityLevelDALC As New VisibilityLevelDALC

        Try

            ' retornar el objeto
            addVisibilityLevel = VisibilityLevelDALC.add(objApplicationCredentials, VisibilityLevel)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addVisibilityLevel")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un VisibilityLevel. ")

        Finally
            ' liberando recursos
            VisibilityLevelDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un VisibilityLevel por el Id
    ''' </summary>
    ''' <param name="idVisibilityLevel"></param>
    ''' <remarks></remarks>
    Public Function loadVisibilityLevel(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idVisibilityLevel As Integer) As VisibilityLevelEntity

        ' definir los objetos
        Dim VisibilityLevelDALC As New VisibilityLevelDALC

        Try

            ' retornar el objeto
            loadVisibilityLevel = VisibilityLevelDALC.load(objApplicationCredentials, idVisibilityLevel)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadVisibilityLevel")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un VisibilityLevel. ")

        Finally
            ' liberando recursos
            VisibilityLevelDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo VisibilityLevel
    ''' </summary>
    ''' <param name="VisibilityLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateVisibilityLevel(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal VisibilityLevel As VisibilityLevelEntity) As Long

        ' definir los objetos
        Dim VisibilityLevelDALC As New VisibilityLevelDALC

        Try

            ' retornar el objeto
            updateVisibilityLevel = VisibilityLevelDALC.update(objApplicationCredentials, VisibilityLevel)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateVisibilityLevel")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un VisibilityLevel. ")

        Finally
            ' liberando recursos
            VisibilityLevelDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el VisibilityLevel de una forma
    ''' </summary>
    ''' <param name="idVisibilityLevel"></param>
    ''' <remarks></remarks>
    Public Sub deleteVisibilityLevel(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idVisibilityLevel As Integer)

        ' definir los objetos
        Dim VisibilityLevelDALC As New VisibilityLevelDALC

        Try

            ' retornar el objeto
            VisibilityLevelDALC.delete(objApplicationCredentials, idVisibilityLevel)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteVisibilityLevel")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un VisibilityLevel. ")

        Finally
            ' liberando recursos
            VisibilityLevelDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Summoning"


    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifySummoningCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim Summoning As New SummoningDALC

        Try

            ' retornar el objeto
            verifySummoningCode = Summoning.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de Summoning. ")

        Finally
            ' liberando recursos
            Summoning = Nothing

        End Try

    End Function


    ''' <summary>
    ''' Obtener la lista de Summoning registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSummoningList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
          Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal idproject As String = "", _
          Optional ByVal projectname As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of SummoningEntity)

        ' definir los objetos
        Dim Summoning As New SummoningDALC

        Try

            ' retornar el objeto
            getSummoningList = Summoning.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             description, _
             idproject, _
            projectname, _
             begindate, _
             enddate, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getSummoningList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Summoning. ")

        Finally
            ' liberando recursos
            Summoning = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Summoning
    ''' </summary>
    ''' <param name="Summoning"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addSummoning(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Summoning As SummoningEntity) As Long

        ' definir los objetos
        Dim SummoningDALC As New SummoningDALC

        Try

            ' retornar el objeto
            addSummoning = SummoningDALC.add(objApplicationCredentials, Summoning)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addSummoning")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Summoning. ")

        Finally
            ' liberando recursos
            SummoningDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Summoning por el Id
    ''' </summary>
    ''' <param name="idSummoning"></param>
    ''' <remarks></remarks>
    Public Function loadSummoning(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSummoning As Integer) As SummoningEntity

        ' definir los objetos
        Dim SummoningDALC As New SummoningDALC

        Try

            ' retornar el objeto
            loadSummoning = SummoningDALC.load(objApplicationCredentials, idSummoning)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadSummoning")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Summoning. ")

        Finally
            ' liberando recursos
            SummoningDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Summoning
    ''' </summary>
    ''' <param name="Summoning"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateSummoning(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Summoning As SummoningEntity) As Long

        ' definir los objetos
        Dim SummoningDALC As New SummoningDALC

        Try

            ' retornar el objeto
            updateSummoning = SummoningDALC.update(objApplicationCredentials, Summoning)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateSummoning")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Summoning. ")

        Finally
            ' liberando recursos
            SummoningDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Summoning de una forma
    ''' </summary>
    ''' <param name="idSummoning"></param>
    ''' <remarks></remarks>
    Public Sub deleteSummoning(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSummoning As Integer)

        ' definir los objetos
        Dim SummoningDALC As New SummoningDALC

        Try

            ' retornar el objeto
            SummoningDALC.delete(objApplicationCredentials, idSummoning)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteSummoning")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Summoning. ")

        Finally
            ' liberando recursos
            SummoningDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ContractRequest"

    ''' <summary>
    ''' Obtener la lista de ContractRequest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getContractRequestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal requestnumber As String = "", _
        Optional ByVal requestnumberlike As String = "", _
        Optional ByVal idmanagement As String = "", _
        Optional ByVal managementname As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal idcontractnature As String = "", _
        Optional ByVal contractnaturename As String = "", _
        Optional ByVal contractnumberadjusted As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal filterfinish As String = "", _
        Optional ByVal order As String = "") As List(Of ContractRequestEntity)

        ' definir los objetos
        Dim ContractRequest As New ContractRequestDALC

        Try

            ' retornar el objeto
            getContractRequestList = ContractRequest.getList(objApplicationCredentials, _
             requestnumber, _
             requestnumberlike, _
             idmanagement, _
             managementname, _
             idproject, _
             projectname, _
             idcontractnature, _
             contractnaturename, _
             contractnumberadjusted, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             filterfinish, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getContractRequestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Solicitudes de contrato. ")

        Finally
            ' liberando recursos
            ContractRequest = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ContractRequest
    ''' </summary>
    ''' <param name="ContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractRequest As ContractRequestEntity) As Long

        ' definir los objetos
        Dim ContractRequestDALC As New ContractRequestDALC
        Dim miIdContractRequest As Long

        Try

            'Se llama al metodo que almacena la informacion general de la solicitud del contrato
            miIdContractRequest = ContractRequestDALC.add(objApplicationCredentials, ContractRequest)

            'Se recorre la lista de contratistas
            For Each contractorLegalEntity As ContractorLegalEntityByContractRequestEntity In ContractRequest.CONTRACTORLEGALENTITYBYCONTRACTREQUESTLIST
                'Por cada elemento de la lista
                'Se agrega el id de la tabla principal 
                contractorLegalEntity.idcontractrequest = miIdContractRequest
                'Se llama al metodo que almacena la informacion de los contratistas personas juridicas
                Me.addContractorLegalEntityByContractRequest(objApplicationCredentials, contractorLegalEntity)
            Next


            ''Se recorre la lista de contratistas personas naturales
            'For Each contractorNaturalPerson As ContractorNaturalPersonByContractRequestEntity In ContractRequest.CONTRACTORNATURALPERSONBYCONTRACTREQUESTLIST
            '    'Por cada elemento de la lista
            '    'Se agrega el id de la tabla principal 
            '    contractorNaturalPerson.idcontractrequest = miIdContractRequest
            '    'Se llama al metodo que almacena la informacion de los contratistas personas naturales
            '    Me.addContractorNaturalPersonByContractRequest(objApplicationCredentials, contractorNaturalPerson)
            'Next

            ''Se llama al metodo que permite almacenar la información de Objeto y valor para la solicitud de contrato actual
            'ContractRequest.SUBJECTANDVALUEBYCONTRACTREQUEST.idcontractrequest = miIdContractRequest
            'Me.addSubjectAndValueByContractRequest(objApplicationCredentials, ContractRequest.SUBJECTANDVALUEBYCONTRACTREQUEST)

            ''Se recorre la lista de pagos de la solicitud de contrato actual
            'For Each paymentsList As PaymentsListByContractRequestEntity In ContractRequest.PAYMENTSLISTBYCONTRACTREQUESTLIST
            '    'Por cada elemento de la lista
            '    'Se agrega el id de la tabla principal 
            '    paymentsList.idcontractrequest = miIdContractRequest
            '    'Se llama al metodo que almacena la informacion de la listta de pagos de la solicitud de contrato actual
            '    Me.addPaymentsListByContractRequest(objApplicationCredentials, paymentsList)
            'Next

            'Se llama al metodo que permite almacenar la información de los datos del contrato para la solicitud de contrato actual
            ContractRequest.CONTRACTDATABYCONTRACTREQUEST.idcontractrequest = miIdContractRequest
            Me.addContractDataByContractRequest(objApplicationCredentials, ContractRequest.CONTRACTDATABYCONTRACTREQUEST)

            ''Se llama al metodo que permite almacenar la información de las observaciones para la solicitud de contrato actual
            'ContractRequest.COMMENTSBYCONTRACTREQUEST.idcontractrequest = miIdContractRequest
            'Me.addCommentsByContractRequest(objApplicationCredentials, ContractRequest.COMMENTSBYCONTRACTREQUEST)

            ' finalizar la transaccion
            CtxSetComplete()

            Return miIdContractRequest

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar una Solicitud de contrato. ")

        Finally
            ' liberando recursos
            ContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal requestNumber As Integer) As ContractRequestEntity

        ' definir los objetos
        Dim ContractRequestDALC As New ContractRequestDALC
        Dim contractRequestEntity As ContractRequestEntity

        Try

            'Se llama al metodo que permite poblar la información de la solicitud de contrato actual
            contractRequestEntity = ContractRequestDALC.load(objApplicationCredentials, requestNumber)

            'Se llama al metodo que pemite cargar la lista de constratistas personas jurídicas
            contractRequestEntity.CONTRACTORLEGALENTITYBYCONTRACTREQUESTLIST = Me.getContractorLegalEntityByContractRequestList(objApplicationCredentials, idcontractrequest:=contractRequestEntity.requestnumber)

            'Se llama al metodo que pemite cargar la lista de constratistas personas natturales
            'contractRequestEntity.CONTRACTORNATURALPERSONBYCONTRACTREQUESTLIST = Me.getContractorNaturalPersonByContractRequestList(objApplicationCredentials, idcontractrequest:=contractRequestEntity.requestnumber)

            'Se llama al metodo que permite consultar la información  del Objeto y vaor de la solicitud de contrato actual
            'contractRequestEntity.SUBJECTANDVALUEBYCONTRACTREQUEST = Me.loadSubjectAndValueByContractRequest(objApplicationCredentials, requestNumber)

            'Se llama al metodo que pemite cargar la lista de de pagos de la solicitud de contrato actual
            'contractRequestEntity.PAYMENTSLISTBYCONTRACTREQUESTLIST = Me.getPaymentsListByContractRequestList(objApplicationCredentials, idcontractrequest:=contractRequestEntity.requestnumber)

            'Se llama al metodo que permite consultar la información  de los datos del contrato de la solicitud de contrato actual
            contractRequestEntity.CONTRACTDATABYCONTRACTREQUEST = Me.loadContractDataByContractRequest(objApplicationCredentials, requestNumber)

            'Se llama al metodo que permite consultar la información  de las observaciones de la solicitud de contrato actual
            'contractRequestEntity.COMMENTSBYCONTRACTREQUEST = Me.loadCommentsByContractRequest(objApplicationCredentials, requestNumber)

            ' finalizar la transaccion
            CtxSetComplete()

            Return contractRequestEntity

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una Solicitud de contrato. " & ex.Message)

        Finally
            ' liberando recursos
            ContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractRequest
    ''' </summary>
    ''' <param name="ContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractRequest As ContractRequestEntity) As Long

        ' definir los objetos
        Dim ContractRequestDALC As New ContractRequestDALC
        Dim miIdContractRequest As Long

        Try

            'Se llama al metodo que actualiza la informacion de la solicitud de contrato actual
            miIdContractRequest = ContractRequestDALC.update(objApplicationCredentials, ContractRequest)

            ''Se elimina la información existente de los contratistas personas juridicas
            'Me.deleteAllContractorLegalEntityByContractRequest(objApplicationCredentials, ContractRequest.requestnumber)
            ''Se recorre la lista de contratistas personas juridicas
            'For Each contractorLegalEntity As ContractorLegalEntityByContractRequestEntity In ContractRequest.CONTRACTORLEGALENTITYBYCONTRACTREQUESTLIST
            '    'Por cada elemento de la lista
            '    'Se agrega el id de la tabla principal 
            '    contractorLegalEntity.idcontractrequest = ContractRequest.requestnumber
            '    'Se llama al metodo que almacena la informacion de los contratistas personas juridicas
            '    Me.addContractorLegalEntityByContractRequest(objApplicationCredentials, contractorLegalEntity)
            'Next

            ''Se elimina la información existente de los contratistas personas naturales
            'Me.deleteAllContractorNaturalPersonByContractRequest(objApplicationCredentials, ContractRequest.requestnumber)
            ''Se recorre la lista de contratistas personas naturales
            'For Each contractorNaturalPerson As ContractorNaturalPersonByContractRequestEntity In ContractRequest.CONTRACTORNATURALPERSONBYCONTRACTREQUESTLIST
            '    'Por cada elemento de la lista
            '    'Se agrega el id de la tabla principal 
            '    contractorNaturalPerson.idcontractrequest = ContractRequest.requestnumber
            '    'Se llama al metodo que almacena la informacion de los contratistas personas naturales
            '    Me.addContractorNaturalPersonByContractRequest(objApplicationCredentials, contractorNaturalPerson)
            'Next

            ''Se llama al metodo que permite almacenar la información de Objeto y valor para la solicitud de contrato actual
            'ContractRequest.SUBJECTANDVALUEBYCONTRACTREQUEST.idcontractrequest = ContractRequest.requestnumber
            'Me.updateSubjectAndValueByContractRequest(objApplicationCredentials, ContractRequest.SUBJECTANDVALUEBYCONTRACTREQUEST)

            ''Se elimina la información existente de las listas de pago de la solicitud de contrato actual
            'Me.deleteAllPaymentsListByContractRequest(objApplicationCredentials, ContractRequest.requestnumber)
            ''Se recorre la lista de pagos de la solicitud de contrato actual
            'For Each paymentsList As PaymentsListByContractRequestEntity In ContractRequest.PAYMENTSLISTBYCONTRACTREQUESTLIST
            '    'Por cada elemento de la lista
            '    'Se agrega el id de la tabla principal 
            '    paymentsList.idcontractrequest = ContractRequest.requestnumber
            '    'Se llama al metodo que almacena la informacion de la listta de pagos de la solicitud de contrato actual
            '    Me.addPaymentsListByContractRequest(objApplicationCredentials, paymentsList)
            'Next

            'Se llama al metodo que permite almacenar la información de los datos del contrato para la solicitud de contrato actual
            ContractRequest.CONTRACTDATABYCONTRACTREQUEST.idcontractrequest = ContractRequest.requestnumber
            Me.updateContractDataByContractRequest(objApplicationCredentials, ContractRequest.CONTRACTDATABYCONTRACTREQUEST)

            'Se llama al metodo que permite almacenar la información de las observaciones para la solicitud de contrato actual
            'ContractRequest.COMMENTSBYCONTRACTREQUEST.idcontractrequest = ContractRequest.requestnumber
            'Me.updateCommentsByContractRequest(objApplicationCredentials, ContractRequest.COMMENTSBYCONTRACTREQUEST)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar una Solicitud de contrato. " & ex.Message)

        Finally
            ' liberando recursos
            ContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractRequest de una forma
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Sub deleteContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal requestNumber As Integer)

        ' definir los objetos
        Dim ContractRequestDALC As New ContractRequestDALC

        Try

            'Se elimina la información existente de la lista de contratistas personas jurídicas
            Me.deleteAllContractorLegalEntityByContractRequest(objApplicationCredentials, requestNumber)

            'Se elimina la información existente de la lista de contratistas personas narurales
            Me.deleteAllContractorNaturalPersonByContractRequest(objApplicationCredentials, requestNumber)

            'Se llama al metodo que elimina al Objeto y valor de la solicitud de contrato actual
            Me.deleteSubjectAndValueByContractRequest(objApplicationCredentials, requestNumber)

            'Se elimina la información existente de la lista de pagos de la solocitud de contrato actual
            Me.deleteAllPaymentsListByContractRequest(objApplicationCredentials, requestNumber)

            'Se llama al metodo que elimina los datos del contrato de la solicitud de contrato actual
            Me.deleteContractDataByContractRequest(objApplicationCredentials, requestNumber)

            'Se llama al metodo que elimina los datos de las observaciones de la solicitud de contrato actual
            Me.deleteCommentsByContractRequest(objApplicationCredentials, requestNumber)

            'Se  llama al metodo que permite eliminar la solicitud de contrato actual
            ContractRequestDALC.delete(objApplicationCredentials, requestNumber)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una Solicitud de contrato. ")

        Finally
            ' liberando recursos
            ContractRequestDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Permite consultar la información de los proyectos
    ''' segun una gerencia determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idManagement">identificador de la gerencia</param>
    ''' <param name="enabled">estado del registro</param>
    ''' <param name="order">orden de los campos</param>
    ''' <returns>lista de objetos de tipo ProjectEntity</returns>
    ''' <remarks></remarks>
    Public Function getProjectByManagementList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idManagement As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idphase As String = "") As List(Of ProjectEntity)



        ' definir los objetos
        Dim ContractRequest As New ContractRequestDALC

        Try

            ' retornar el objeto
            getProjectByManagementList = ContractRequest.getListByManagement(objApplicationCredentials, idManagement, enabled, order, idphase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getContractRequestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Proyectos. ")

        Finally
            ' liberando recursos
            ContractRequest = Nothing

        End Try

    End Function


#End Region

#Region "ContractorLegalEntityByContractRequest"

    ''' <summary>
    ''' Obtener la lista de ContractorLegalEntityByContractRequest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getContractorLegalEntityByContractRequestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal entitynamedescription As String = "", _
        Optional ByVal nit As String = "", _
        Optional ByVal legalrepresentative As String = "", _
        Optional ByVal contractorname As String = "", _
        Optional ByVal identificationnumber As String = "", _
        Optional ByVal order As String = "") As List(Of ContractorLegalEntityByContractRequestEntity)

        ' definir los objetos
        Dim ContractorLegalEntityByContractRequest As New ContractorLegalEntityByContractRequestDALC

        Try

            ' retornar el objeto
            getContractorLegalEntityByContractRequestList = ContractorLegalEntityByContractRequest.getList(objApplicationCredentials, _
             id, _
             idcontractrequest, _
             entitynamedescription, _
             nit, _
             legalrepresentative, _
             contractorname, _
             identificationnumber, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getContractorLegalEntityByContractRequestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ContractorLegalEntityByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorLegalEntityByContractRequest = Nothing

        End Try

    End Function

    Public Function getPolizaDetailsList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idpoliza As Integer) As List(Of PolizaDetailsEntity)

        'definir los objetos
        Dim polizadetails As New PolizaDetailsDALC

        Try

            getPolizaDetailsList = polizadetails.getList(objApplicationCredentials, idpoliza)

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getContractorLegalEntityByContractRequestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de conceptos de una poliza. ")

        Finally
            ' liberando recursos
            polizadetails = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ContractorLegalEntityByContractRequest
    ''' </summary>
    ''' <param name="ContractorLegalEntityByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addContractorLegalEntityByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractorLegalEntityByContractRequest As ContractorLegalEntityByContractRequestEntity) As Long

        ' definir los objetos
        Dim ContractorLegalEntityByContractRequestDALC As New ContractorLegalEntityByContractRequestDALC

        Try

            ' retornar el objeto
            addContractorLegalEntityByContractRequest = ContractorLegalEntityByContractRequestDALC.add(objApplicationCredentials, ContractorLegalEntityByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addContractorLegalEntityByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ContractorLegalEntityByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorLegalEntityByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractorLegalEntityByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractorLegalEntityByContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadContractorLegalEntityByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractorLegalEntityByContractRequest As Integer) As ContractorLegalEntityByContractRequestEntity

        ' definir los objetos
        Dim ContractorLegalEntityByContractRequestDALC As New ContractorLegalEntityByContractRequestDALC

        Try

            ' retornar el objeto
            loadContractorLegalEntityByContractRequest = ContractorLegalEntityByContractRequestDALC.load(objApplicationCredentials, idContractorLegalEntityByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadContractorLegalEntityByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ContractorLegalEntityByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorLegalEntityByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractorLegalEntityByContractRequest
    ''' </summary>
    ''' <param name="ContractorLegalEntityByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateContractorLegalEntityByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractorLegalEntityByContractRequest As ContractorLegalEntityByContractRequestEntity) As Long

        ' definir los objetos
        Dim ContractorLegalEntityByContractRequestDALC As New ContractorLegalEntityByContractRequestDALC

        Try

            ' retornar el objeto
            updateContractorLegalEntityByContractRequest = ContractorLegalEntityByContractRequestDALC.update(objApplicationCredentials, ContractorLegalEntityByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateContractorLegalEntityByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ContractorLegalEntityByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorLegalEntityByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractorLegalEntityByContractRequest de una forma
    ''' </summary>
    ''' <param name="idContractorLegalEntityByContractRequest"></param>
    ''' <remarks></remarks>
    Public Sub deleteContractorLegalEntityByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractorLegalEntityByContractRequest As Integer)

        ' definir los objetos
        Dim ContractorLegalEntityByContractRequestDALC As New ContractorLegalEntityByContractRequestDALC

        Try

            ' retornar el objeto
            ContractorLegalEntityByContractRequestDALC.delete(objApplicationCredentials, idContractorLegalEntityByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteContractorLegalEntityByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ContractorLegalEntityByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorLegalEntityByContractRequestDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra todos los registros almacenados de los contratistas personas jurídicas de una solicitud determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="idRequestNumber">identificador de la soliciutd de contrato</param>
    ''' <remarks></remarks>
    Public Sub deleteAllContractorLegalEntityByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idRequestNumber As Integer)

        ' definir los objetos
        Dim contractorLegalEntityByContractRequestDALC As New ContractorLegalEntityByContractRequestDALC

        Try

            'Se llama al metodo que permite eliminar la lista de contratistas personas jurídicas de la solicitud de contrato actual
            contractorLegalEntityByContractRequestDALC.deleteAll(objApplicationCredentials, idRequestNumber)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteLocationByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar la lista de contratistas personas jurídicas de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            contractorLegalEntityByContractRequestDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ContractorNaturalPersonByContractRequest"

    ''' <summary>
    ''' Obtener la lista de ContractorNaturalPersonByContractRequest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getContractorNaturalPersonByContractRequestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal nit As String = "", _
        Optional ByVal contractorname As String = "", _
        Optional ByVal order As String = "") As List(Of ContractorNaturalPersonByContractRequestEntity)

        ' definir los objetos
        Dim ContractorNaturalPersonByContractRequest As New ContractorNaturalPersonByContractRequestDALC

        Try

            ' retornar el objeto
            getContractorNaturalPersonByContractRequestList = ContractorNaturalPersonByContractRequest.getList(objApplicationCredentials, _
             id, _
             idcontractrequest, _
             nit, _
             contractorname, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getContractorNaturalPersonByContractRequestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ContractorNaturalPersonByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorNaturalPersonByContractRequest = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ContractorNaturalPersonByContractRequest
    ''' </summary>
    ''' <param name="ContractorNaturalPersonByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addContractorNaturalPersonByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractorNaturalPersonByContractRequest As ContractorNaturalPersonByContractRequestEntity) As Long

        ' definir los objetos
        Dim ContractorNaturalPersonByContractRequestDALC As New ContractorNaturalPersonByContractRequestDALC

        Try

            ' retornar el objeto
            addContractorNaturalPersonByContractRequest = ContractorNaturalPersonByContractRequestDALC.add(objApplicationCredentials, ContractorNaturalPersonByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addContractorNaturalPersonByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ContractorNaturalPersonByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorNaturalPersonByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractorNaturalPersonByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractorNaturalPersonByContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadContractorNaturalPersonByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractorNaturalPersonByContractRequest As Integer) As ContractorNaturalPersonByContractRequestEntity

        ' definir los objetos
        Dim ContractorNaturalPersonByContractRequestDALC As New ContractorNaturalPersonByContractRequestDALC

        Try

            ' retornar el objeto
            loadContractorNaturalPersonByContractRequest = ContractorNaturalPersonByContractRequestDALC.load(objApplicationCredentials, idContractorNaturalPersonByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadContractorNaturalPersonByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ContractorNaturalPersonByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorNaturalPersonByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractorNaturalPersonByContractRequest
    ''' </summary>
    ''' <param name="ContractorNaturalPersonByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateContractorNaturalPersonByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractorNaturalPersonByContractRequest As ContractorNaturalPersonByContractRequestEntity) As Long

        ' definir los objetos
        Dim ContractorNaturalPersonByContractRequestDALC As New ContractorNaturalPersonByContractRequestDALC

        Try

            ' retornar el objeto
            updateContractorNaturalPersonByContractRequest = ContractorNaturalPersonByContractRequestDALC.update(objApplicationCredentials, ContractorNaturalPersonByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateContractorNaturalPersonByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ContractorNaturalPersonByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorNaturalPersonByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractorNaturalPersonByContractRequest de una forma
    ''' </summary>
    ''' <param name="idContractorNaturalPersonByContractRequest"></param>
    ''' <remarks></remarks>
    Public Sub deleteContractorNaturalPersonByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractorNaturalPersonByContractRequest As Integer)

        ' definir los objetos
        Dim ContractorNaturalPersonByContractRequestDALC As New ContractorNaturalPersonByContractRequestDALC

        Try

            ' retornar el objeto
            ContractorNaturalPersonByContractRequestDALC.delete(objApplicationCredentials, idContractorNaturalPersonByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteContractorNaturalPersonByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ContractorNaturalPersonByContractRequest. ")

        Finally
            ' liberando recursos
            ContractorNaturalPersonByContractRequestDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra todos los registros almacenados de los contratistas personas naturales de una solicitud determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="idRequestNumber">identificador de la soliciutd de contrato</param>
    ''' <remarks></remarks>
    Public Sub deleteAllContractorNaturalPersonByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idRequestNumber As Integer)

        ' definir los objetos
        Dim contractorNaturalPersonByContractRequestDALC As New ContractorNaturalPersonByContractRequestDALC

        Try

            'Se llama al metodo que permite eliminar la lista de contratistas personas naturales de la solicitud de contrato actual
            contractorNaturalPersonByContractRequestDALC.deleteAll(objApplicationCredentials, idRequestNumber)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteLocationByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar la lista de contratistas personas naturales de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            contractorNaturalPersonByContractRequestDALC = Nothing

        End Try

    End Sub

#End Region

#Region "SubjectAndValueByContractRequest"

    ''' <summary>
    ''' Obtener la lista de SubjectAndValueByContractRequest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSubjectAndValueByContractRequestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal subjectcontract As String = "", _
        Optional ByVal productsordeliverables As String = "", _
        Optional ByVal contractvalue As String = "", _
        Optional ByVal contributionamount As String = "", _
        Optional ByVal feesconsultantbyinstitution As String = "", _
        Optional ByVal totalfeesintegralconsultant As String = "", _
        Optional ByVal contributionamountrecipientinstitution As String = "", _
        Optional ByVal idcurrency As String = "", _
        Optional ByVal order As String = "") As List(Of SubjectAndValueByContractRequestEntity)

        ' definir los objetos
        Dim SubjectAndValueByContractRequest As New SubjectAndValueByContractRequestDALC

        Try

            ' retornar el objeto
            getSubjectAndValueByContractRequestList = SubjectAndValueByContractRequest.getList(objApplicationCredentials, _
             id, _
             idcontractrequest, _
             subjectcontract, _
             productsordeliverables, _
             contractvalue, _
             contributionamount, _
             feesconsultantbyinstitution, _
             totalfeesintegralconsultant, _
             contributionamountrecipientinstitution, _
             idcurrency, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getSubjectAndValueByContractRequestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Objetos y valores de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            SubjectAndValueByContractRequest = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo SubjectAndValueByContractRequest
    ''' </summary>
    ''' <param name="SubjectAndValueByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addSubjectAndValueByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal SubjectAndValueByContractRequest As SubjectAndValueByContractRequestEntity) As Long

        ' definir los objetos
        Dim SubjectAndValueByContractRequestDALC As New SubjectAndValueByContractRequestDALC

        Try

            ' retornar el objeto
            addSubjectAndValueByContractRequest = SubjectAndValueByContractRequestDALC.add(objApplicationCredentials, SubjectAndValueByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addSubjectAndValueByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar el Objeto y valor de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            SubjectAndValueByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un SubjectAndValueByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadSubjectAndValueByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer) As SubjectAndValueByContractRequestEntity

        ' definir los objetos
        Dim SubjectAndValueByContractRequestDALC As New SubjectAndValueByContractRequestDALC

        Try

            ' retornar el objeto
            loadSubjectAndValueByContractRequest = SubjectAndValueByContractRequestDALC.load(objApplicationCredentials, idContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadSubjectAndValueByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el Objeto y valor de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            SubjectAndValueByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo SubjectAndValueByContractRequest
    ''' </summary>
    ''' <param name="SubjectAndValueByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateSubjectAndValueByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal SubjectAndValueByContractRequest As SubjectAndValueByContractRequestEntity) As Long

        ' definir los objetos
        Dim SubjectAndValueByContractRequestDALC As New SubjectAndValueByContractRequestDALC

        Try

            ' retornar el objeto
            updateSubjectAndValueByContractRequest = SubjectAndValueByContractRequestDALC.update(objApplicationCredentials, SubjectAndValueByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateSubjectAndValueByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el Objeto y valor de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            SubjectAndValueByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el SubjectAndValueByContractRequest de una forma
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Sub deleteSubjectAndValueByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer)

        ' definir los objetos
        Dim SubjectAndValueByContractRequestDALC As New SubjectAndValueByContractRequestDALC

        Try

            ' retornar el objeto
            SubjectAndValueByContractRequestDALC.delete(objApplicationCredentials, idContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteSubjectAndValueByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar el Objeto y valor de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            SubjectAndValueByContractRequestDALC = Nothing

        End Try

    End Sub

#End Region

#Region "PaymentsListByContractRequest"

    ''' <summary>
    ''' Obtener la lista de PaymentsListByContractRequest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getPaymentsListByContractRequestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal value As String = "", _
        Optional ByVal percentage As String = "", _
        Optional ByVal datePaymentsList As String = "", _
        Optional ByVal order As String = "") As List(Of PaymentsListByContractRequestEntity)

        ' definir los objetos
        Dim PaymentsListByContractRequest As New PaymentsListByContractRequestDALC

        Try

            ' retornar el objeto
            getPaymentsListByContractRequestList = PaymentsListByContractRequest.getList(objApplicationCredentials, _
             id, _
             idcontractrequest, _
             value, _
             percentage, _
             datePaymentsList, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getPaymentsListByContractRequestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de pagos de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            PaymentsListByContractRequest = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo PaymentsListByContractRequest
    ''' </summary>
    ''' <param name="PaymentsListByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addPaymentsListByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal PaymentsListByContractRequest As PaymentsListByContractRequestEntity) As Long

        ' definir los objetos
        Dim PaymentsListByContractRequestDALC As New PaymentsListByContractRequestDALC

        Try

            ' retornar el objeto
            addPaymentsListByContractRequest = PaymentsListByContractRequestDALC.add(objApplicationCredentials, PaymentsListByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addPaymentsListByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar la lista de pagos de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            PaymentsListByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un PaymentsListByContractRequest por el Id
    ''' </summary>
    ''' <param name="idPaymentsListByContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadPaymentsListByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idPaymentsListByContractRequest As Integer) As PaymentsListByContractRequestEntity

        ' definir los objetos
        Dim PaymentsListByContractRequestDALC As New PaymentsListByContractRequestDALC

        Try

            ' retornar el objeto
            loadPaymentsListByContractRequest = PaymentsListByContractRequestDALC.load(objApplicationCredentials, idPaymentsListByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadPaymentsListByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de pagos de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            PaymentsListByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo PaymentsListByContractRequest
    ''' </summary>
    ''' <param name="PaymentsListByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updatePaymentsListByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal PaymentsListByContractRequest As PaymentsListByContractRequestEntity) As Long

        ' definir los objetos
        Dim PaymentsListByContractRequestDALC As New PaymentsListByContractRequestDALC

        Try

            ' retornar el objeto
            updatePaymentsListByContractRequest = PaymentsListByContractRequestDALC.update(objApplicationCredentials, PaymentsListByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updatePaymentsListByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar la lista de pagos de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            PaymentsListByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el PaymentsListByContractRequest de una forma
    ''' </summary>
    ''' <param name="idPaymentsListByContractRequest"></param>
    ''' <remarks></remarks>
    Public Sub deletePaymentsListByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idPaymentsListByContractRequest As Integer)

        ' definir los objetos
        Dim PaymentsListByContractRequestDALC As New PaymentsListByContractRequestDALC

        Try

            ' retornar el objeto
            PaymentsListByContractRequestDALC.delete(objApplicationCredentials, idPaymentsListByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deletePaymentsListByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar la lista de pagos de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            PaymentsListByContractRequestDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra todos los registros almacenados de los pagos de una solicitud determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="idRequestNumber">identificador de la soliciutd de contrato</param>
    ''' <remarks></remarks>
    Public Sub deleteAllPaymentsListByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idRequestNumber As Integer)

        ' definir los objetos
        Dim paymentsListByContractRequestDALC As New PaymentsListByContractRequestDALC

        Try

            'Se llama al metodo que permite eliminar la lista de contratistas personas naturales de la solicitud de contrato actual
            paymentsListByContractRequestDALC.deleteAll(objApplicationCredentials, idRequestNumber)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteLocationByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar la lista de pagos de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            paymentsListByContractRequestDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Metodo que permite actualziar lal ista de pagos desde
    ''' la ejecución del contrato
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="PaymentsListByContractRequest">objeto de tipo PaymentsListByContractRequestEntity</param>
    ''' <remarks></remarks>
    Public Sub updatePaymentsListByContractRequestContractExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal PaymentsListByContractRequest As PaymentsListByContractRequestEntity)

        ' definir los objetos
        Dim PaymentsListByContractRequestDALC As New PaymentsListByContractRequestDALC

        Try

            ' retornar el objeto
            PaymentsListByContractRequestDALC.updateByContractExecution(objApplicationCredentials, PaymentsListByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updatePaymentsListByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar la lista de pagos de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            PaymentsListByContractRequestDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ContractDataByContractRequest"

    ''' <summary>
    ''' Obtener la lista de ContractDataByContractRequest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getContractDataByContractRequestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal contractduration As String = "", _
        Optional ByVal startdate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal supervisor As String = "", _
        Optional ByVal budgetvalidity As String = "", _
        Optional ByVal contactdata As String = "", _
        Optional ByVal email As String = "", _
        Optional ByVal telephone As String = "", _
        Optional ByVal order As String = "") As List(Of ContractDataByContractRequestEntity)

        ' definir los objetos
        Dim ContractDataByContractRequest As New ContractDataByContractRequestDALC

        Try

            ' retornar el objeto
            getContractDataByContractRequestList = ContractDataByContractRequest.getList(objApplicationCredentials, _
             id, _
             idcontractrequest, _
             contractduration, _
             startdate, _
             enddate, _
             supervisor, _
             budgetvalidity, _
             contactdata, _
             email, _
             telephone, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getContractDataByContractRequestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los datos del contrato de la solicitud actual. ")

        Finally
            ' liberando recursos
            ContractDataByContractRequest = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ContractDataByContractRequest
    ''' </summary>
    ''' <param name="ContractDataByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addContractDataByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractDataByContractRequest As ContractDataByContractRequestEntity) As Long

        ' definir los objetos
        Dim ContractDataByContractRequestDALC As New ContractDataByContractRequestDALC

        Try

            ' retornar el objeto
            addContractDataByContractRequest = ContractDataByContractRequestDALC.add(objApplicationCredentials, ContractDataByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addContractDataByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar los datos del contrato de la solicitud actual. ")

        Finally
            ' liberando recursos
            ContractDataByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractDataByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadContractDataByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer) As ContractDataByContractRequestEntity

        ' definir los objetos
        Dim ContractDataByContractRequestDALC As New ContractDataByContractRequestDALC

        Try

            ' retornar el objeto
            loadContractDataByContractRequest = ContractDataByContractRequestDALC.load(objApplicationCredentials, idContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadContractDataByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los datos del contrato de la solicitud actual. ")

        Finally
            ' liberando recursos
            ContractDataByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractDataByContractRequest
    ''' </summary>
    ''' <param name="ContractDataByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateContractDataByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractDataByContractRequest As ContractDataByContractRequestEntity) As Long

        ' definir los objetos
        Dim ContractDataByContractRequestDALC As New ContractDataByContractRequestDALC

        Try

            ' retornar el objeto
            updateContractDataByContractRequest = ContractDataByContractRequestDALC.update(objApplicationCredentials, ContractDataByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateContractDataByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar los datos del contrato de la solicitud actual. ")

        Finally
            ' liberando recursos
            ContractDataByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractDataByContractRequest de una forma
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Sub deleteContractDataByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer)

        ' definir los objetos
        Dim ContractDataByContractRequestDALC As New ContractDataByContractRequestDALC

        Try

            ' retornar el objeto
            ContractDataByContractRequestDALC.delete(objApplicationCredentials, idContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteContractDataByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar los datos del contrato de la solicitud actual. ")

        Finally
            ' liberando recursos
            ContractDataByContractRequestDALC = Nothing

        End Try

    End Sub

#End Region

#Region "CommentsByContractRequest"

    ''' <summary>
    ''' Obtener la lista de CommentsByContractRequest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCommentsByContractRequestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal additionalcomments As String = "", _
        Optional ByVal startactrequires As String = "", _
        Optional ByVal datenoticeexpiration As String = "", _
        Optional ByVal contractnumber As String = "", _
        Optional ByVal purchaseorder As String = "", _
        Optional ByVal order As String = "") As List(Of CommentsByContractRequestEntity)

        ' definir los objetos
        Dim CommentsByContractRequest As New CommentsByContractRequestDALC

        Try

            ' retornar el objeto
            getCommentsByContractRequestList = CommentsByContractRequest.getList(objApplicationCredentials, _
             id, _
             idcontractrequest, _
             additionalcomments, _
             startactrequires, _
             datenoticeexpiration, _
             contractnumber, _
             purchaseorder, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getCommentsByContractRequestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar las observaciones de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            CommentsByContractRequest = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo CommentsByContractRequest
    ''' </summary>
    ''' <param name="CommentsByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addCommentsByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal CommentsByContractRequest As CommentsByContractRequestEntity) As Long

        ' definir los objetos
        Dim CommentsByContractRequestDALC As New CommentsByContractRequestDALC

        Try

            ' retornar el objeto
            addCommentsByContractRequest = CommentsByContractRequestDALC.add(objApplicationCredentials, CommentsByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addCommentsByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar las observaciones de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            CommentsByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un CommentsByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadCommentsByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer) As CommentsByContractRequestEntity

        ' definir los objetos
        Dim CommentsByContractRequestDALC As New CommentsByContractRequestDALC

        Try

            ' retornar el objeto
            loadCommentsByContractRequest = CommentsByContractRequestDALC.load(objApplicationCredentials, idContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadCommentsByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar las observaciones de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            CommentsByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo CommentsByContractRequest
    ''' </summary>
    ''' <param name="CommentsByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateCommentsByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal CommentsByContractRequest As CommentsByContractRequestEntity) As Long

        ' definir los objetos
        Dim CommentsByContractRequestDALC As New CommentsByContractRequestDALC

        Try

            ' retornar el objeto
            updateCommentsByContractRequest = CommentsByContractRequestDALC.update(objApplicationCredentials, CommentsByContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateCommentsByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar las observaciones de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            CommentsByContractRequestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el CommentsByContractRequest de una forma
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Sub deleteCommentsByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer)

        ' definir los objetos
        Dim CommentsByContractRequestDALC As New CommentsByContractRequestDALC

        Try

            ' retornar el objeto
            CommentsByContractRequestDALC.delete(objApplicationCredentials, idContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteCommentsByContractRequest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar las observaciones de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            CommentsByContractRequestDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ContractExecution"

    ''' <summary>
    ''' Obtener la lista de ContractExecution registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getContractExecutionList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal startdate As String = "", _
        Optional ByVal paymentdate As String = "", _
        Optional ByVal contractnumber As String = "", _
        Optional ByVal ordernumber As String = "", _
        Optional ByVal closingcomments As String = "", _
        Optional ByVal closingdate As String = "", _
        Optional ByVal finalpaymentdate As String = "", _
        Optional ByVal value As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ContractExecutionEntity)

        ' definir los objetos
        Dim ContractExecution As New ContractExecutionDALC

        Try

            ' retornar el objeto
            getContractExecutionList = ContractExecution.getList(objApplicationCredentials, _
             idcontractrequest, _
             startdate, _
             paymentdate, _
             contractnumber, _
             ordernumber, _
             closingcomments, _
             closingdate, _
             finalpaymentdate, _
             value, _
             iduser, _
             username, _
             createdate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getContractExecutionList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista las Ejecuciones de contrato. ")

        Finally
            ' liberando recursos
            ContractExecution = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ContractExecution
    ''' </summary>
    ''' <param name="ContractExecution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addContractExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractExecution As ContractExecutionEntity) As Long

        ' definir los objetos
        Dim ContractExecutionDALC As New ContractExecutionDALC

        Try

            ' retornar el objeto
            addContractExecution = ContractExecutionDALC.add(objApplicationCredentials, ContractExecution)

            'Se verifica que exista una lista de pagos
            If Not (ContractExecution.PAYMENTSLIST Is Nothing) Then

                'Se recorre la lista de pagos
                For Each objPaymentList As PaymentsListByContractRequestEntity In ContractExecution.PAYMENTSLIST
                    'Se llama al metodo que actualiza la lista de pagos de la ejecución de contrato actual
                    Me.updatePaymentsListByContractRequestContractExecution(objApplicationCredentials, objPaymentList)
                Next

            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addContractExecution")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar una Ejecución de contrato. ")

        Finally
            ' liberando recursos
            ContractExecutionDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractExecution por el Id
    ''' </summary>
    ''' <param name="idContractExecution"></param>
    ''' <remarks></remarks>
    Public Function loadContractExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractExecution As Integer) As ContractExecutionEntity

        ' definir los objetos
        Dim ContractExecutionDALC As New ContractExecutionDALC

        Try

            ' retornar el objeto
            loadContractExecution = ContractExecutionDALC.load(objApplicationCredentials, idContractExecution)

            'Se llama al metodo que carga la lista de pagos de la ejecución de contrato actual
            loadContractExecution.PAYMENTSLIST = Me.getPaymentsListByContractRequestList(objApplicationCredentials, _
                idcontractrequest:=loadContractExecution.idcontractrequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadContractExecution")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una Ejecución de contrato. ")

        Finally
            ' liberando recursos
            ContractExecutionDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractExecution
    ''' </summary>
    ''' <param name="ContractExecution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateContractExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractExecution As ContractExecutionEntity) As Long

        ' definir los objetos
        Dim ContractExecutionDALC As New ContractExecutionDALC

        Try

            'Se llama al método qeu actualiza la ejecución de contrato actual
            updateContractExecution = ContractExecutionDALC.update(objApplicationCredentials, ContractExecution)

            'Se verifica que exista una lista de pagos
            If Not (ContractExecution.PAYMENTSLIST Is Nothing) Then

                'Se recorre la lista de pagos
                For Each objPaymentList As PaymentsListByContractRequestEntity In ContractExecution.PAYMENTSLIST
                    'Se llama al metodo que actualiza la lista de pagos de la ejecución de contrato actual
                    Me.updatePaymentsListByContractRequestContractExecution(objApplicationCredentials, objPaymentList)
                Next

            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateContractExecution")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar una Ejecución de contrato. ")

        Finally
            ' liberando recursos
            ContractExecutionDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractExecution de una forma
    ''' </summary>
    ''' <param name="idContractExecution"></param>
    ''' <remarks></remarks>
    Public Sub deleteContractExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractExecution As Integer)

        ' definir los objetos
        Dim ContractExecutionDALC As New ContractExecutionDALC

        Try

            ' retornar el objeto
            ContractExecutionDALC.delete(objApplicationCredentials, idContractExecution)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteContractExecution")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una Ejecución de contrato. ")

        Finally
            ' liberando recursos
            ContractExecutionDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Metodo que permite poblar una lista de solicitudes de contrato
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="enabled">estado de los registros</param>
    ''' <returns>retorna una lista de solicitudes de contrato</returns>
    ''' <remarks></remarks>
    Public Function getContractExecutionContractRequestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal enabled As String = "") As List(Of ContractRequestEntity)

        ' definir los objetos
        Dim ContractExecution As New ContractExecutionDALC

        Try

            ' retornar el objeto
            getContractExecutionContractRequestList = ContractExecution.getContractRequestList(objApplicationCredentials, enabled)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getContractExecutionList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista las Solicitudes de contrato. ")

        Finally
            ' liberando recursos
            ContractExecution = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="IdContractRequest">Identificador que se desea verificar</param>
    ''' <returns>Verdadero si existe algún registro, falso en caso contrario</returns>
    ''' <remarks></remarks>
    Public Function verifyContractExecutionCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal IdContractRequest As String) As Boolean

        ' definir los objetos
        Dim ContractExecutionDALC As New ContractExecutionDALC

        Try

            ' retornar el objeto
            verifyContractExecutionCode = ContractExecutionDALC.verifyCode(objApplicationCredentials, IdContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el Identificador de la Ejecución de contrato actual. ")

        Finally
            ' liberando recursos
            ContractExecutionDALC = Nothing

        End Try

    End Function

#End Region

#Region "Proposal"

    ''' <summary>
    ''' Obtener la lista de Proposal registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProposalList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idLike As String = "", _
        Optional ByVal idsummoning As String = "", _
        Optional ByVal summoningName As String = "", _
        Optional ByVal nameOperator As String = "", _
        Optional ByVal operatornit As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal target As String = "", _
        Optional ByVal targetpopulation As String = "", _
        Optional ByVal expectedresults As String = "", _
        Optional ByVal totalvalue As String = "", _
        Optional ByVal inputfsc As String = "", _
        Optional ByVal inputothersources As String = "", _
        Optional ByVal briefprojectdescription As String = "", _
        Optional ByVal score As String = "", _
        Optional ByVal result As String = "", _
        Optional ByVal responsiblereview As String = "", _
        Optional ByVal reviewdate As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal deptoName As String = "", _
        Optional ByVal cityName As String = "", _
        Optional ByVal order As String = "") As List(Of ProposalEntity)

        ' definir los objetos
        Dim Proposal As New ProposalDALC

        Try

            ' retornar el objeto
            getProposalList = Proposal.getList(objApplicationCredentials, _
             id, _
             idLike, _
             idsummoning, _
             summoningName, _
             nameOperator, _
             operatornit, _
             projectname, _
             target, _
             targetpopulation, _
             expectedresults, _
             totalvalue, _
             inputfsc, _
             inputothersources, _
             briefprojectdescription, _
             score, _
             result, _
             responsiblereview, _
             reviewdate, _
             enabled, _
             createdate, _
             deptoName, _
             cityName, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProposalList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de propuestas.")

        Finally
            ' liberando recursos
            Proposal = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Proposal
    ''' </summary>
    ''' <param name="Proposal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addProposal(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Proposal As ProposalEntity) As Long

        ' definir los objetos
        Dim ProposalDALC As New ProposalDALC
        Dim miIdProposal As Integer

        Try

            ' retornar el objeto
            miIdProposal = ProposalDALC.add(objApplicationCredentials, Proposal)

            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (Proposal.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In Proposal.DOCUMENTLIST
                    'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                    If (document.attachfile.Length > 0) Then

                        'Se instancia un objeto de tipo documento por entidad
                        Dim documentByEntity As New DocumentsByEntityEntity()

                        'Se almacena el documento y se recupera su Id
                        documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                        documentByEntity.idnentity = miIdProposal
                        documentByEntity.entityname = Proposal.GetType.ToString()

                        'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                        Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        document.ISNEW = False
                    End If
                Next
            End If

            'Se recorre la lista de ubicaciones por idea
            For Each location As LocationByProposalEntity In Proposal.LOCATIONSLIST
                'Por cada elemento de la lista
                'Se agrega el id de la idea 
                location.idproposal = miIdProposal
                'Se llama al metodo que almacena la informacion de las ubicaciones por idea.
                Me.addLocationByProposal(objApplicationCredentials, location)
            Next

            ' devolver
            addProposal = miIdProposal

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addProposal")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un propuesta.")

        Finally
            ' liberando recursos
            ProposalDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Proposal por el Id
    ''' </summary>
    ''' <param name="idProposal"></param>
    ''' <remarks></remarks>
    Public Function loadProposal(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProposal As Integer) As ProposalEntity

        ' definir los objetos
        Dim ProposalDALC As New ProposalDALC

        Try

            ' retornar el objeto
            loadProposal = ProposalDALC.load(objApplicationCredentials, idProposal)

            'Se carga la lista de documentos para el registro de idea actual
            loadProposal.DOCUMENTSBYENTITYLIST = Me.getDocumentsByEntityList(objApplicationCredentials, idnentity:=idProposal, entityName:=GetType(ProposalEntity).ToString())

            'Se verifica que existan documentos anexos al registro actual
            If (Not loadProposal.DOCUMENTSBYENTITYLIST Is Nothing AndAlso loadProposal.DOCUMENTSBYENTITYLIST.Count > 0) Then
                'Se recorre la lista de identificadores de documentos agregados
                Dim idsDocuments As String = ""
                For Each documentByEntity As DocumentsByEntityEntity In loadProposal.DOCUMENTSBYENTITYLIST
                    idsDocuments &= documentByEntity.iddocuments.ToString() & ","
                Next
                If (idsDocuments.Length > 0) Then idsDocuments = idsDocuments.Substring(0, idsDocuments.Length - 1)

                'Se carga la lista de documentos requeridos
                loadProposal.DOCUMENTLIST = Me.getDocumentsListByEntity(objApplicationCredentials, idsDocuments)
            End If

            'Se llama al metodo que pemite cargar la lista ubicaciones por propuesta
            loadProposal.LOCATIONSLIST = getLocationByProposalList(objApplicationCredentials, idproposal:=loadProposal.id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadProposal")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una propuesta.")

        Finally
            ' liberando recursos
            ProposalDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Proposal
    ''' </summary>
    ''' <param name="Proposal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProposal(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Proposal As ProposalEntity) As Long

        ' definir los objetos
        Dim ProposalDALC As New ProposalDALC

        Try

            ' retornar el objeto
            updateProposal = ProposalDALC.update(objApplicationCredentials, Proposal)

            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (Proposal.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In Proposal.DOCUMENTLIST

                    'Se verifica si el archivo actual es un archivo nuevo
                    If (document.ISNEW) Then

                        'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                        If (document.attachfile.Length > 0) Then

                            'Se instancia un objeto de tipo documento por entidad
                            Dim documentByEntity As New DocumentsByEntityEntity()

                            'Se almacena el documento y se recupera su Id
                            documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                            documentByEntity.idnentity = Proposal.id
                            documentByEntity.entityname = Proposal.GetType.ToString()

                            'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                            Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        End If

                        'Se verifica si el archivo ha sido borrado
                    ElseIf (document.ISDELETED) Then

                        'Se llama al metodo que permite eliminar el registro de documentos por entidad
                        Me.deleteAllDocumentsByDocumentAndEntity(objApplicationCredentials, document.id, Proposal.id, Proposal.GetType.ToString())

                        'Se llama al metodo que permite eliminar el documento requerido
                        Me.deleteDocuments(objApplicationCredentials, document.id, document.attachfile)

                    End If
                Next
            End If

            'Se elimina la informacion existente de las ubicaciones para la propuesta actual
            Me.deleteAllLocationByProposal(objApplicationCredentials, Proposal.id)
            'Se recorre la lista de ubicaciones por propuesta
            For Each locationByEntity As LocationByProposalEntity In Proposal.LOCATIONSLIST
                'Por cada elemento de la lista se agrega el id de la propuesta 
                locationByEntity.idproposal = Proposal.id
                'Se llama al metodo que almacena la informacion de las ubicaciones por propuesta.
                Me.addLocationByProposal(objApplicationCredentials, locationByEntity)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProposal")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar una propuesta.")

        Finally
            ' liberando recursos
            ProposalDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Proposal de una forma
    ''' </summary>
    ''' <param name="idProposal"></param>
    ''' <remarks></remarks>
    Public Sub deleteProposal(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProposal As Integer)

        ' definir los objetos
        Dim ProposalDALC As New ProposalDALC

        Try

            ' retornar el objeto
            ProposalDALC.delete(objApplicationCredentials, idProposal)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProposal")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una propuesta.")

        Finally
            ' liberando recursos
            ProposalDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra todas las ubicaciones de una Propuesta
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idProposal">identificador de la propuesta</param>
    ''' <remarks></remarks>
    Public Sub deleteAllLocationByProposal(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProposal As Integer)

        ' definir los objetos
        Dim LocationByProposalDALC As New LocationByProposalDALC

        Try

            ' retornar el objeto
            LocationByProposalDALC.deleteAll(objApplicationCredentials, idProposal)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteLocationByIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar la lista de ubicaciones por idea. ")

        Finally
            ' liberando recursos
            LocationByProposalDALC = Nothing

        End Try

    End Sub

#End Region

#Region "LocationByProposal"

    ''' <summary>
    ''' Obtener la lista de LocationByProposal registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getLocationByProposalList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idproposal As String = "", _
        Optional ByVal iddepto As String = "", _
        Optional ByVal idcity As String = "", _
        Optional ByVal order As String = "") As List(Of LocationByProposalEntity)

        ' definir los objetos
        Dim LocationByProposal As New LocationByProposalDALC

        Try

            ' retornar el objeto
            getLocationByProposalList = LocationByProposal.getList(objApplicationCredentials, _
             id, _
             idproposal, _
             iddepto, _
             idcity, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getLocationByProposalList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de LocationByProposal. ")

        Finally
            ' liberando recursos
            LocationByProposal = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo LocationByProposal
    ''' </summary>
    ''' <param name="LocationByProposal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addLocationByProposal(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal LocationByProposal As LocationByProposalEntity) As Long

        ' definir los objetos
        Dim LocationByProposalDALC As New LocationByProposalDALC

        Try

            ' retornar el objeto
            addLocationByProposal = LocationByProposalDALC.add(objApplicationCredentials, LocationByProposal)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addLocationByProposal")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un LocationByProposal. ")

        Finally
            ' liberando recursos
            LocationByProposalDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un LocationByProposal por el Id
    ''' </summary>
    ''' <param name="idLocationByProposal"></param>
    ''' <remarks></remarks>
    Public Function loadLocationByProposal(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idLocationByProposal As Integer) As LocationByProposalEntity

        ' definir los objetos
        Dim LocationByProposalDALC As New LocationByProposalDALC

        Try

            ' retornar el objeto
            loadLocationByProposal = LocationByProposalDALC.load(objApplicationCredentials, idLocationByProposal)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadLocationByProposal")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un LocationByProposal. ")

        Finally
            ' liberando recursos
            LocationByProposalDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo LocationByProposal
    ''' </summary>
    ''' <param name="LocationByProposal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateLocationByProposal(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal LocationByProposal As LocationByProposalEntity) As Long

        ' definir los objetos
        Dim LocationByProposalDALC As New LocationByProposalDALC

        Try

            ' retornar el objeto
            updateLocationByProposal = LocationByProposalDALC.update(objApplicationCredentials, LocationByProposal)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateLocationByProposal")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un LocationByProposal. ")

        Finally
            ' liberando recursos
            LocationByProposalDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el LocationByProposal de una forma
    ''' </summary>
    ''' <param name="idLocationByProposal"></param>
    ''' <remarks></remarks>
    Public Sub deleteLocationByProposal(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idLocationByProposal As Integer)

        ' definir los objetos
        Dim LocationByProposalDALC As New LocationByProposalDALC

        Try

            ' retornar el objeto
            LocationByProposalDALC.delete(objApplicationCredentials, idLocationByProposal)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteLocationByProposal")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un LocationByProposal. ")

        Finally
            ' liberando recursos
            LocationByProposalDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Execution"

    ''' <summary>
    ''' Obtener la lista de Execution registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getExecutionList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idproject As String = "", _
         Optional ByVal projectname As String = "", _
        Optional ByVal qualitativeindicators As String = "", _
        Optional ByVal learning As String = "", _
        Optional ByVal Adjust As String = "", _
           Optional ByVal achievements As String = "", _
        Optional ByVal TestimonyName As String = "", _
          Optional ByVal enable As String = "", _
           Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal order As String = "") As List(Of ExecutionEntity)

        ' definir los objetos
        Dim Execution As New ExecutionDALC

        Try

            ' retornar el objeto
            getExecutionList = Execution.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idproject, _
             projectname, _
             qualitativeindicators, _
             learning, _
             Adjust, _
             achievements, _
             TestimonyName, _
             enable, _
             enabledtext, _
             iduser, _
             createdate, _
             username, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getExecutionList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Execution. ")

        Finally
            ' liberando recursos
            Execution = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Execution
    ''' </summary>
    ''' <param name="Execution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Execution As ExecutionEntity) As Long

        ' definir los objetos
        Dim ExecutionDALC As New ExecutionDALC
        Dim miIdExecution As Long

        Try

            ' retornar el objeto
            miIdExecution = ExecutionDALC.add(objApplicationCredentials, Execution)

            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (Execution.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In Execution.DOCUMENTLIST
                    'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                    If (document.attachfile.Length > 0) Then

                        'Se instancia un objeto de tipo documento por entidad
                        Dim documentByEntity As New DocumentsByEntityEntity()

                        'Se almacena el documento y se recupera su Id
                        documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                        documentByEntity.idnentity = miIdExecution
                        documentByEntity.entityname = Execution.GetType.ToString()

                        'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                        Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                    End If
                Next
            End If

            'Se recorre la lista de testimonios por ejecucion
            For Each testimony As TestimonyEntity In Execution.TESTIMONYLIST
                'Por cada elemento de la lista
                'Se agrega el id de la idea 
                testimony.idexecution = miIdExecution
                'Se llama al metodo que almacena la informacion de los terceros por idea.
                Me.addTestimony(objApplicationCredentials, testimony)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addExecution")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Execution. ")

        Finally
            ' liberando recursos
            ExecutionDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Execution por el Id
    ''' </summary>
    ''' <param name="idExecution"></param>
    ''' <remarks></remarks>
    Public Function loadExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecution As Integer) As ExecutionEntity

        ' definir los objetos
        Dim ExecutionDALC As New ExecutionDALC

        Try

            ' retornar el objeto
            loadExecution = ExecutionDALC.load(objApplicationCredentials, idExecution)

            'Se carga la lista de documentos para el registro de idea actual
            loadExecution.DOCUMENTSBYEXECUTIONLIST = Me.getDocumentsByEntityList(objApplicationCredentials, idnentity:=idExecution, entityName:=GetType(ExecutionEntity).ToString())

            'Se verifica que existam documentos anexos al registro actual
            If (Not loadExecution.DOCUMENTSBYEXECUTIONLIST Is Nothing AndAlso loadExecution.DOCUMENTSBYEXECUTIONLIST.Count > 0) Then
                'Se recorre la lista de identificadores de documentos agregados
                Dim idsDocuments As String = ""
                For Each documentByEntity As DocumentsByEntityEntity In loadExecution.DOCUMENTSBYEXECUTIONLIST
                    idsDocuments &= documentByEntity.iddocuments.ToString() & ","
                Next
                If (idsDocuments.Length > 0) Then idsDocuments = idsDocuments.Substring(0, idsDocuments.Length - 1)

                'Se carga la lista de documentos requeridos
                loadExecution.DOCUMENTLIST = Me.getDocumentsListByEntity(objApplicationCredentials, idsDocuments)
            Else
                loadExecution.DOCUMENTLIST = New List(Of DocumentsEntity)
            End If

            'Se llama al metodo que pemite cargar la lista de testimonio por ejecución
            loadExecution.TESTIMONYLIST = getTestimonyList(objApplicationCredentials, idexecution:=loadExecution.id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadExecution")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Execution. ")

        Finally
            ' liberando recursos
            ExecutionDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Execution
    ''' </summary>
    ''' <param name="Execution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Execution As ExecutionEntity) As Long

        ' definir los objetos
        Dim ExecutionDALC As New ExecutionDALC

        Try

            ' retornar el objeto
            updateExecution = ExecutionDALC.update(objApplicationCredentials, Execution)

            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (Execution.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In Execution.DOCUMENTLIST

                    'Se verifica si el archivo actual es un archivo nuevo
                    If (document.ISNEW) Then

                        'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                        If (document.attachfile.Length > 0) Then

                            'Se instancia un objeto de tipo documento por entidad
                            Dim documentByEntity As New DocumentsByEntityEntity()

                            'Se almacena el documento y se recupera su Id
                            documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                            documentByEntity.idnentity = Execution.id
                            documentByEntity.entityname = Execution.GetType.ToString()

                            'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                            Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        End If

                        'Se verifica si el archivo ha sido borrado
                    ElseIf (document.ISDELETED) Then

                        'Se llama al metodo que permite eliminar el registro de documentos por entidad
                        Me.deleteAllDocumentsByDocumentAndEntity(objApplicationCredentials, document.id, Execution.id, Execution.GetType.ToString())

                        'Se llama al metodo que permite eliminar el documento requerido
                        Me.deleteDocuments(objApplicationCredentials, document.id, document.attachfile)

                    End If
                Next
            End If


            'Se elimina la informacion existente de los testiomonios para la ejecución actual
            Me.deleteAllTestimony(objApplicationCredentials, Execution.id)

            'Se recorre la lista de testimonios por ejecución
            For Each Testimony As TestimonyEntity In Execution.TESTIMONYLIST
                'Por cada elemento de la lista
                'Se agrega el id del testimonio
                Testimony.idexecution = Execution.id
                'Se llama al metodo que almacena la informacion de los testimonios por ejecución.
                Me.addTestimony(objApplicationCredentials, Testimony)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateExecution")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Execution. ")

        Finally
            ' liberando recursos
            ExecutionDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Execution de una forma
    ''' </summary>
    ''' <param name="idExecution"></param>
    ''' <remarks></remarks>
    Public Sub deleteExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecution As Integer, ByVal documentsList As List(Of DocumentsEntity))

        ' definir los objetos
        Dim ExecutionDALC As New ExecutionDALC

        Try

            'Se recorre la lista fisica de documentos adjuntos a la idea actual
            For Each document As DocumentsEntity In documentsList

                'Se elimina la lista de documentos adjuntos
                Me.deleteDocuments(objApplicationCredentials, document.id, document.attachfile)

            Next

            'Se elimina en la B.D. la lista de documentos por entidad
            Me.deleteAllDocumentsByEntity(objApplicationCredentials, idExecution, GetType(ExecutionEntity).ToString())

            'Se elimina la informacion existente de los testimonios para la ejecución actual
            Me.deleteAllTestimony(objApplicationCredentials, idExecution)

            ' retornar el objeto
            ExecutionDALC.delete(objApplicationCredentials, idExecution)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteExecution")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Execution. ")

        Finally
            ' liberando recursos
            ExecutionDALC = Nothing

        End Try

    End Sub

    ''' <summary> 
    ''' Registar un nuevo Testimony
    ''' </summary>
    ''' <param name="Testimony"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addTestimony(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Testimony As TestimonyEntity) As Long

        ' definir los objetos
        Dim TestimonyDALC As New TestimonyDALC

        Try

            ' retornar el objeto
            addTestimony = TestimonyDALC.add(objApplicationCredentials, Testimony)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addTestimomny")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Testimony. ")

        Finally
            ' liberando recursos
            Testimony = Nothing

        End Try

    End Function
    ''' <summary> Retorna los testimonios de una ejecución
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="id"></param>
    ''' <param name="idexecution"></param>
    ''' <param name="name"></param>
    ''' <param name="age"></param>
    ''' <param name="sex"></param>
    ''' <param name="phone"></param>
    ''' <param name="idcity"></param>
    ''' <param name="description"></param>
    ''' <param name="email"></param>
    ''' <param name="projectrole"></param>
    ''' <param name="order"></param>
    ''' <returns>n objeto de tipo List(Of TestimonyList)</returns>
    ''' <remarks></remarks>
    Public Function getTestimonyList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idexecution As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal age As String = "", _
        Optional ByVal sex As String = "", _
        Optional ByVal phone As String = "", _
        Optional ByVal idcity As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal email As String = "", _
        Optional ByVal projectrole As String = "", _
        Optional ByVal order As String = "") As List(Of TestimonyEntity)
        ' definir los objetos
        Dim Testimony As New TestimonyDALC

        Try

            ' retornar el objeto
            getTestimonyList = Testimony.getList(objApplicationCredentials, _
             id, _
             idexecution, _
            name, _
             age, _
            sex, _
            phone, _
            idcity, _
            description, _
            email, _
            projectrole, _
            order)
            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getTestimonyList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Testimony. ")

        Finally
            ' liberando recursos
            Testimony = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Borra los testimonios por ejecución  determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idExecution">identificador del registro de ejecución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteAllTestimony(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecution As Integer) As Long
        ' definir los objetos
        Dim TestimonyDALC As New TestimonyDALC

        Try

            ' retornar el objeto
            TestimonyDALC.deleteAll(objApplicationCredentials, idExecution)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteTestimony")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Testimony. ")

        Finally
            ' liberando recursos
            TestimonyDALC = Nothing

        End Try
    End Function
#End Region

#Region "SubActivityInformationRegistry"

    ''' <summary>
    ''' Obtener la lista de SubActivityInformationRegistry registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSubActivityInformationRegistryList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idsubactivity As String = "", _
        Optional ByVal subactivityname As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal order As String = "") As List(Of SubActivityInformationRegistryEntity)

        ' definir los objetos
        Dim SubActivityInformationRegistry As New SubActivityInformationRegistryDALC

        Try

            ' retornar el objeto
            getSubActivityInformationRegistryList = SubActivityInformationRegistry.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idsubactivity, _
             subactivityname, _
             description, _
             begindate, _
             enddate, _
             comments, _
             attachment, _
             iduser, _
             username, _
             createdate, _
             enabled, _
             enabledtext, _
   order)

            For Each objSAIR As SubActivityInformationRegistryEntity In getSubActivityInformationRegistryList
                objSAIR.DOCUMENTSBYSAIRELIST = Me.getDocumentsByEntityList(objApplicationCredentials, idnentity:=objSAIR.id, entityName:=GetType(SubActivityInformationRegistryEntity).ToString())
                'Se verifica que existam documentos anexos al registro actual
                If (Not objSAIR.DOCUMENTSBYSAIRELIST Is Nothing AndAlso objSAIR.DOCUMENTSBYSAIRELIST.Count > 0) Then
                    'Se recorre la lista de identificadores de documentos agregados
                    Dim idsDocuments As String = ""
                    For Each documentByEntity As DocumentsByEntityEntity In objSAIR.DOCUMENTSBYSAIRELIST
                        idsDocuments &= documentByEntity.iddocuments.ToString() & ","
                    Next
                    If (idsDocuments.Length > 0) Then idsDocuments = idsDocuments.Substring(0, idsDocuments.Length - 1)

                    'Se carga la lista de documentos requeridos
                    objSAIR.DOCUMENTLIST = Me.getDocumentsListByEntity(objApplicationCredentials, idsDocuments)
                End If
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getSubActivityInformationRegistryList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de SubActivityInformationRegistry. ")

        Finally
            ' liberando recursos
            SubActivityInformationRegistry = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo SubActivityInformationRegistry
    ''' </summary>
    ''' <param name="SubActivityInformationRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addSubActivityInformationRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal SubActivityInformationRegistry As SubActivityInformationRegistryEntity) As Long

        ' definir los objetos
        Dim SubActivityInformationRegistryDALC As New SubActivityInformationRegistryDALC

        Try

            ' retornar el objeto
            addSubActivityInformationRegistry = SubActivityInformationRegistryDALC.add(objApplicationCredentials, SubActivityInformationRegistry)


            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (SubActivityInformationRegistry.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In SubActivityInformationRegistry.DOCUMENTLIST
                    'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                    If (document.attachfile.Length > 0) Then

                        'Se instancia un objeto de tipo documento por entidad
                        Dim documentByEntity As New DocumentsByEntityEntity()

                        'Se almacena el documento y se recupera su Id
                        documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                        documentByEntity.idnentity = addSubActivityInformationRegistry
                        documentByEntity.entityname = SubActivityInformationRegistry.GetType.ToString()

                        'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                        Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        document.ISNEW = False
                    End If
                Next
            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addSubActivityInformationRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un SubActivityInformationRegistry. ")

        Finally
            ' liberando recursos
            SubActivityInformationRegistryDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un SubActivityInformationRegistry por el Id
    ''' </summary>
    ''' <param name="idSubActivityInformationRegistry"></param>
    ''' <remarks></remarks>
    Public Function loadSubActivityInformationRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSubActivityInformationRegistry As Integer) As SubActivityInformationRegistryEntity

        ' definir los objetos
        Dim SubActivityInformationRegistryDALC As New SubActivityInformationRegistryDALC

        Try

            ' retornar el objeto
            loadSubActivityInformationRegistry = SubActivityInformationRegistryDALC.load(objApplicationCredentials, idSubActivityInformationRegistry)

            loadSubActivityInformationRegistry.DOCUMENTSBYSAIRELIST = Me.getDocumentsByEntityList(objApplicationCredentials, idnentity:=idSubActivityInformationRegistry, entityName:=GetType(SubActivityInformationRegistryEntity).ToString())


            'Se verifica que existam documentos anexos al registro actual
            If (Not loadSubActivityInformationRegistry.DOCUMENTSBYSAIRELIST Is Nothing AndAlso loadSubActivityInformationRegistry.DOCUMENTSBYSAIRELIST.Count > 0) Then
                'Se recorre la lista de identificadores de documentos agregados
                Dim idsDocuments As String = ""
                For Each documentByEntity As DocumentsByEntityEntity In loadSubActivityInformationRegistry.DOCUMENTSBYSAIRELIST
                    idsDocuments &= documentByEntity.iddocuments.ToString() & ","
                Next
                If (idsDocuments.Length > 0) Then idsDocuments = idsDocuments.Substring(0, idsDocuments.Length - 1)

                'Se carga la lista de documentos requeridos
                loadSubActivityInformationRegistry.DOCUMENTLIST = Me.getDocumentsListByEntity(objApplicationCredentials, idsDocuments)
            End If



            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadSubActivityInformationRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un SubActivityInformationRegistry. ")

        Finally
            ' liberando recursos
            SubActivityInformationRegistryDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo SubActivityInformationRegistry
    ''' </summary>
    ''' <param name="SubActivityInformationRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateSubActivityInformationRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal SubActivityInformationRegistry As SubActivityInformationRegistryEntity) As Long

        ' definir los objetos
        Dim SubActivityInformationRegistryDALC As New SubActivityInformationRegistryDALC

        Try

            ' retornar el objeto
            updateSubActivityInformationRegistry = SubActivityInformationRegistryDALC.update(objApplicationCredentials, SubActivityInformationRegistry)

            'Se verifica que exista una lista de documentos cargados en el servidor
            If Not (SubActivityInformationRegistry.DOCUMENTLIST Is Nothing) Then
                'Se recorre la lista de documentos anexos
                For Each document As DocumentsEntity In SubActivityInformationRegistry.DOCUMENTLIST

                    'Se verifica si el archivo actual es un archivo nuevo
                    If (document.ISNEW) Then

                        'Se verifica para el archivo actual que exista un nombre de archivo adjunto
                        If (document.attachfile.Length > 0) Then

                            'Se instancia un objeto de tipo documento por entidad
                            Dim documentByEntity As New DocumentsByEntityEntity()

                            'Se almacena el documento y se recupera su Id
                            documentByEntity.iddocuments = Me.addDocuments(objApplicationCredentials, document)
                            documentByEntity.idnentity = SubActivityInformationRegistry.id
                            documentByEntity.entityname = SubActivityInformationRegistry.GetType.ToString()

                            'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                            Me.addDocumentsByEntity(objApplicationCredentials, documentByEntity)
                        End If

                        'Se verifica si el archivo ha sido borrado
                    ElseIf (document.ISDELETED) Then

                        'Se llama al metodo que permite eliminar el registro de documentos por entidad
                        Me.deleteAllDocumentsByDocumentAndEntity(objApplicationCredentials, document.id, SubActivityInformationRegistry.id, SubActivityInformationRegistry.GetType.ToString())

                        'Se llama al metodo que permite eliminar el documento requerido
                        Me.deleteDocuments(objApplicationCredentials, document.id, document.attachfile)

                    End If
                Next
            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateSubActivityInformationRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un SubActivityInformationRegistry. ")

        Finally
            ' liberando recursos
            SubActivityInformationRegistryDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el SubActivityInformationRegistry de una forma
    ''' </summary>
    ''' <param name="idSubActivityInformationRegistry"></param>
    ''' <remarks></remarks>
    Public Sub deleteSubActivityInformationRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSubActivityInformationRegistry As Integer, ByVal documentsList As List(Of DocumentsEntity))

        ' definir los objetos
        Dim SubActivityInformationRegistryDALC As New SubActivityInformationRegistryDALC

        Try

            'Se recorre la lista fisica de documentos adjuntos a la idea actual
            For Each document As DocumentsEntity In documentsList

                'Se elimina la lista de documentos adjuntos
                Me.deleteDocuments(objApplicationCredentials, document.id, document.attachfile)

            Next

            'Se elimina en la B.D. la lista de documentos por entidad
            Me.deleteAllDocumentsByEntity(objApplicationCredentials, idSubActivityInformationRegistry, GetType(SubActivityInformationRegistryEntity).ToString())

            ' retornar el objeto
            SubActivityInformationRegistryDALC.delete(objApplicationCredentials, idSubActivityInformationRegistry)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteSubActivityInformationRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un SubActivityInformationRegistry. ")

        Finally
            ' liberando recursos
            SubActivityInformationRegistryDALC = Nothing

        End Try

    End Sub

#End Region

#Region "IndicatorInformation"

    ''' <summary>
    ''' Obtener la lista de IndicatorInformation registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getIndicatorInformationList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idmeasurementdatebyindicator As String = "", _
        Optional ByVal idindicator As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal goal As String = "", _
        Optional ByVal value As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal registrationdate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal order As String = "") As List(Of IndicatorInformationEntity)

        ' definir los objetos
        Dim IndicatorInformation As New IndicatorInformationDALC

        Try

            ' retornar el objeto
            getIndicatorInformationList = IndicatorInformation.getList(objApplicationCredentials, _
             id, _
             idmeasurementdatebyindicator, _
             idindicator, _
             description, _
             goal, _
             value, _
             comments, _
             registrationdate, _
             iduser, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getIndicatorInformationList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de la informaciónes del indicador.")

        Finally
            ' liberando recursos
            IndicatorInformation = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo IndicatorInformation
    ''' </summary>
    ''' <param name="IndicatorInformation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addIndicatorInformation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal IndicatorInformation As IndicatorInformationEntity) As Long

        ' definir los objetos
        Dim IndicatorInformationDALC As New IndicatorInformationDALC

        Try

            ' retornar el objeto
            addIndicatorInformation = IndicatorInformationDALC.add(objApplicationCredentials, IndicatorInformation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addIndicatorInformation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar la información del indicador.")

        Finally
            ' liberando recursos
            IndicatorInformationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un IndicatorInformation por el Id
    ''' </summary>
    ''' <param name="idIndicatorInformation"></param>
    ''' <remarks></remarks>
    Public Function loadIndicatorInformation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIndicatorInformation As Integer) As IndicatorInformationEntity

        ' definir los objetos
        Dim IndicatorInformationDALC As New IndicatorInformationDALC

        Try

            ' retornar el objeto
            loadIndicatorInformation = IndicatorInformationDALC.load(objApplicationCredentials, idIndicatorInformation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorInformation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la información del indicador.")

        Finally
            ' liberando recursos
            IndicatorInformationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo IndicatorInformation
    ''' </summary>
    ''' <param name="IndicatorInformation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateIndicatorInformation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal IndicatorInformation As IndicatorInformationEntity) As Long

        ' definir los objetos
        Dim IndicatorInformationDALC As New IndicatorInformationDALC

        Try

            ' retornar el objeto
            updateIndicatorInformation = IndicatorInformationDALC.update(objApplicationCredentials, IndicatorInformation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateIndicatorInformation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar la información del indicador.")

        Finally
            ' liberando recursos
            IndicatorInformationDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el IndicatorInformation de una forma
    ''' </summary>
    ''' <param name="idIndicatorInformation"></param>
    ''' <remarks></remarks>
    Public Sub deleteIndicatorInformation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIndicatorInformation As Integer)

        ' definir los objetos
        Dim IndicatorInformationDALC As New IndicatorInformationDALC

        Try

            ' retornar el objeto
            IndicatorInformationDALC.delete(objApplicationCredentials, idIndicatorInformation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteIndicatorInformation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar la información del indicador.")

        Finally
            ' liberando recursos
            IndicatorInformationDALC = Nothing

        End Try

    End Sub

#End Region

#Region "Addressee"

    ''' <summary>
    ''' Obtener la lista de Addressee registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getAddresseeList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal email As String = "", _
        Optional ByVal idusergroup As String = "", _
        Optional ByVal usergroupname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of AddresseeEntity)

        ' definir los objetos
        Dim Addressee As New AddresseeDALC

        Try

            ' retornar el objeto
            getAddresseeList = Addressee.getList(objApplicationCredentials, _
             id, _
             idlike, _
             name, _
             email, _
             idusergroup, _
             usergroupname, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getAddresseeList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Destinatarios. ")

        Finally
            ' liberando recursos
            Addressee = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Addressee
    ''' </summary>
    ''' <param name="Addressee"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addAddressee(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Addressee As AddresseeEntity) As Long

        ' definir los objetos
        Dim AddresseeDALC As New AddresseeDALC

        Try

            ' retornar el objeto
            addAddressee = AddresseeDALC.add(objApplicationCredentials, Addressee)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addAddressee")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Destinatario.")

        Finally
            ' liberando recursos
            AddresseeDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Addressee por el Id
    ''' </summary>
    ''' <param name="idAddressee"></param>
    ''' <remarks></remarks>
    Public Function loadAddressee(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAddressee As Integer) As AddresseeEntity

        ' definir los objetos
        Dim AddresseeDALC As New AddresseeDALC

        Try

            ' retornar el objeto
            loadAddressee = AddresseeDALC.load(objApplicationCredentials, idAddressee)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadAddressee")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Destinatario.")

        Finally
            ' liberando recursos
            AddresseeDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Addressee
    ''' </summary>
    ''' <param name="Addressee"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateAddressee(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Addressee As AddresseeEntity) As Long

        ' definir los objetos
        Dim AddresseeDALC As New AddresseeDALC

        Try

            ' retornar el objeto
            updateAddressee = AddresseeDALC.update(objApplicationCredentials, Addressee)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateAddressee")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Destinatario.")

        Finally
            ' liberando recursos
            AddresseeDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Addressee de una forma
    ''' </summary>
    ''' <param name="idAddressee"></param>
    ''' <remarks></remarks>
    Public Sub deleteAddressee(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAddressee As Integer)

        ' definir los objetos
        Dim AddresseeDALC As New AddresseeDALC

        Try

            ' retornar el objeto
            AddresseeDALC.delete(objApplicationCredentials, idAddressee)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteAddressee")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Destinatario.")

        Finally
            ' liberando recursos
            AddresseeDALC = Nothing

        End Try

    End Sub

#End Region

#Region "UserGroup"

    ''' <summary>
    ''' Obtener la lista de UserGroup registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getUserGroupList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal order As String = "Name", _
        Optional ByVal enabledOnly As Boolean = True) As List(Of UserGroupEntity)

        ' definir los objetos
        Dim UserGroup As New UserGroupDALC

        Try

            ' retornar el objeto
            getUserGroupList = UserGroup.GetUsersGroupList(objApplicationCredentials, _
             order, _
             enabledOnly _
            )

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getUserGroupList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de grupos de usuario.")

        Finally
            ' liberando recursos
            UserGroup = Nothing

        End Try

    End Function

#End Region

#Region "Inquest"

    ''' <summary>
    ''' Obtener la lista de Inquest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getInquestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal projectphase As String = "", _
        Optional ByVal idusergroup As String = "", _
        Optional ByVal usergroupname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of InquestEntity)

        ' definir los objetos
        Dim Inquest As New InquestDALC

        Try

            ' retornar el objeto
            getInquestList = Inquest.getList(objApplicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             idproject, _
             projectname, _
             projectphase, _
             idusergroup, _
             usergroupname, _
             enabled, _
             enabledtext, _
             createdate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getInquestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de encuestas.")

        Finally
            ' liberando recursos
            Inquest = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo Inquest
    ''' </summary>
    ''' <param name="Inquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Inquest As InquestEntity) As Long

        ' definir los objetos
        Dim InquestDALC As New InquestDALC

        Try

            ' retornar el objeto
            addInquest = InquestDALC.add(objApplicationCredentials, Inquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un Inquest. ")

        Finally
            ' liberando recursos
            InquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Inquest por el Id
    ''' </summary>
    ''' <param name="idInquest"></param>
    ''' <remarks></remarks>
    Public Function loadInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquest As Integer) As InquestEntity

        ' definir los objetos
        Dim InquestDALC As New InquestDALC

        Try

            ' retornar el objeto
            loadInquest = InquestDALC.load(objApplicationCredentials, idInquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Inquest. ")

        Finally
            ' liberando recursos
            InquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Inquest
    ''' </summary>
    ''' <param name="Inquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Inquest As InquestEntity) As Long

        ' definir los objetos
        Dim InquestDALC As New InquestDALC

        Try

            ' retornar el objeto
            updateInquest = InquestDALC.update(objApplicationCredentials, Inquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un Inquest. ")

        Finally
            ' liberando recursos
            InquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Inquest de una forma
    ''' </summary>
    ''' <param name="idInquest"></param>
    ''' <remarks></remarks>
    Public Sub deleteInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquest As Integer)

        ' definir los objetos
        Dim InquestDALC As New InquestDALC

        Try

            ' retornar el objeto
            InquestDALC.delete(objApplicationCredentials, idInquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Inquest. ")

        Finally
            ' liberando recursos
            InquestDALC = Nothing

        End Try

    End Sub

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code">codigo a verificar</param>
    ''' <returns>Verdadero si existe algún registro con el mismo código, falso en caso contrario</returns>
    ''' <remarks></remarks>
    Public Function verifyInquestCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim InquestDALC As New InquestDALC

        Try

            ' retornar el objeto
            verifyInquestCode = InquestDALC.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código.")

        Finally
            ' liberando recursos
            InquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite cargar la lista de proyectos existentes en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="enabled">estado del registro</param>
    ''' <param name="order">ordenamiento</param>
    ''' <returns>lista de objetos de tipo ProjectEntity</returns>
    ''' <remarks></remarks>
    Public Function getProjectListByInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal enabled As String, ByVal order As String) As List(Of ProjectEntity)

        ' definir los objetos
        Dim Inquest As New InquestDALC

        Try

            ' retornar el objeto
            getProjectListByInquest = Inquest.getProjectList(objApplicationCredentials, enabled, order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getInquestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de proyectos.")

        Finally
            ' liberando recursos
            Inquest = Nothing

        End Try

    End Function

#End Region

#Region "InquestContent"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code">codigo a verificar</param>
    ''' <returns>Verdadero si existe algún registro con el mismo código, falso en caso contrario</returns>
    ''' <remarks></remarks>
    Public Function verifyInquestContentCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal code As String, _
         Optional ByVal id As String = "") As Boolean

        ' definir los objetos
        Dim InquestContentDALC As New InquestContentDALC

        Try

            ' retornar el objeto
            verifyInquestContentCode = InquestContentDALC.verifyCode(objApplicationCredentials, code, id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código.")

        Finally
            ' liberando recursos
            InquestContentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtener la lista de InquestContent registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getInquestContentList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idinquest As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal questionText As String = "", _
        Optional ByVal questionType As String = "", _
        Optional ByVal answer As String = "", _
        Optional ByVal order As String = "") As List(Of InquestContentEntity)

        ' definir los objetos
        Dim InquestContent As New InquestContentDALC

        Try

            ' retornar el objeto
            getInquestContentList = InquestContent.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idinquest, _
             code, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             questionText, _
             questionType, _
             answer, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getInquestContentList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de contenidos de encuesta.")

        Finally
            ' liberando recursos
            InquestContent = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo InquestContent
    ''' </summary>
    ''' <param name="InquestContent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal InquestContent As InquestContentEntity) As Long

        ' definir los objetos
        Dim InquestContentDALC As New InquestContentDALC
        Dim idInquestContent As Integer
        Dim idQuestionByInquestContent As Integer

        Try

            ' retornar el objeto
            idInquestContent = InquestContentDALC.add(objApplicationCredentials, InquestContent)

            If Not (InquestContent.QUESTIONSLIST Is Nothing) Then
                'Se recorren las preguntas agregadas a la encuesta actual.
                For Each question As QuestionsByInquestContentEntity In InquestContent.QUESTIONSLIST
                    'Se llama al metodo que permite almacenar la iformación de la pregunta.
                    question.idinquestcontent = idInquestContent
                    idQuestionByInquestContent = Me.addQuestionsByInquestContent(objApplicationCredentials, question)

                    If Not (question.ANSWERSLIST Is Nothing) Then
                        'Se recorren las respuestas de la pregunta actual
                        For Each answer As AnswersByQuestionEntity In question.ANSWERSLIST
                            'Se llama al metodo que permite almacenar la inforamción de la respuesta actual
                            answer.idquestionsbyinquestcontent = idQuestionByInquestContent
                            answer.idinquestcontent = idInquestContent
                            Me.addAnswersByQuestion(objApplicationCredentials, answer)
                        Next
                    End If
                Next
            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addInquestContent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un contenido de encuesta.")

        Finally
            ' liberando recursos
            InquestContentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un InquestContent por el Id
    ''' </summary>
    ''' <param name="idInquestContent"></param>
    ''' <remarks></remarks>
    Public Function loadInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquestContent As Integer) As InquestContentEntity

        ' definir los objetos
        Dim InquestContentDALC As New InquestContentDALC

        Try

            ' retornar el objeto
            loadInquestContent = InquestContentDALC.load(objApplicationCredentials, idInquestContent)

            'Se carga la información de la preguntas relacionadas con el contenido de encuesta actual.
            loadInquestContent.QUESTIONSLIST = Me.getQuestionsByInquestContentList(objApplicationCredentials, idinquestcontent:=loadInquestContent.id)

            'Se carga la información de todas las respuestas relacionadas con una pregunta determinada.
            For Each question As QuestionsByInquestContentEntity In loadInquestContent.QUESTIONSLIST
                question.ANSWERSLIST = Me.getAnswersByQuestionList(objApplicationCredentials, idquestionsbyinquestcontent:=question.id)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadInquestContent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un contenido de encuesta.")

        Finally
            ' liberando recursos
            InquestContentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo InquestContent
    ''' </summary>
    ''' <param name="InquestContent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal InquestContent As InquestContentEntity) As Long

        ' definir los objetos
        Dim InquestContentDALC As New InquestContentDALC
        Dim idInquestContent As Integer
        Dim idQuestionByInquestContent As Integer

        Try

            ' retornar el objeto
            idInquestContent = InquestContentDALC.update(objApplicationCredentials, InquestContent)

            'Se elimina la información de todas las respuestas relacionadas con el contenido de encuesta actual.
            Me.deleteAllAnswersByInquestContent(objApplicationCredentials, InquestContent.id)

            'Se elimina la información de todas las preguntas relacionadas con el contenido de encuesta actual.
            Me.deleteAllQuestionsByInquestContent(objApplicationCredentials, InquestContent.id)

            If Not (InquestContent.QUESTIONSLIST Is Nothing) Then
                'Se recorren las preguntas agregadas a la encuesta actual.
                For Each question As QuestionsByInquestContentEntity In InquestContent.QUESTIONSLIST
                    'Se llama al metodo que permite almacenar la iformación de la pregunta.
                    question.idinquestcontent = InquestContent.id
                    idQuestionByInquestContent = Me.addQuestionsByInquestContent(objApplicationCredentials, question)

                    If Not (question.ANSWERSLIST Is Nothing) Then
                        'Se recorren las respuestas de la pregunta actual
                        For Each answer As AnswersByQuestionEntity In question.ANSWERSLIST
                            'Se llama al metodo que permite almacenar la inforamción de la respuesta actual
                            answer.idquestionsbyinquestcontent = idQuestionByInquestContent
                            answer.idinquestcontent = InquestContent.id
                            Me.addAnswersByQuestion(objApplicationCredentials, answer)
                        Next
                    End If
                Next
            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateInquestContent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un contenido de encuesta.")

        Finally
            ' liberando recursos
            InquestContentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el InquestContent de una forma
    ''' </summary>
    ''' <param name="idInquestContent"></param>
    ''' <remarks></remarks>
    Public Sub deleteInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquestContent As Integer)

        ' definir los objetos
        Dim InquestContentDALC As New InquestContentDALC

        Try

            'Se elimina la información de todas las respuestas relacionadas con el contenido de encuesta actual.
            Me.deleteAllAnswersByInquestContent(objApplicationCredentials, idInquestContent)

            'Se elimina la información de todas las preguntas relacionadas con el contenido de encuesta actual.
            Me.deleteAllQuestionsByInquestContent(objApplicationCredentials, idInquestContent)

            ' retornar el objeto
            InquestContentDALC.delete(objApplicationCredentials, idInquestContent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteInquestContent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un contenido de encuesta.")

        Finally
            ' liberando recursos
            InquestContentDALC = Nothing

        End Try

    End Sub

#End Region

#Region "QuestionsByInquestContent"

    ''' <summary>
    ''' Obtener la lista de QuestionsByInquestContent registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getQuestionsByInquestContentList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idinquestcontent As String = "", _
        Optional ByVal questiontext As String = "", _
        Optional ByVal questiontype As String = "", _
        Optional ByVal order As String = "") As List(Of QuestionsByInquestContentEntity)

        ' definir los objetos
        Dim QuestionsByInquestContent As New QuestionsByInquestContentDALC

        Try

            ' retornar el objeto
            getQuestionsByInquestContentList = QuestionsByInquestContent.getList(objApplicationCredentials, _
             id, _
             idinquestcontent, _
             questiontext, _
             questiontype, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getQuestionsByInquestContentList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de preguntas por contenido de encuesta.")

        Finally
            ' liberando recursos
            QuestionsByInquestContent = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo QuestionsByInquestContent
    ''' </summary>
    ''' <param name="QuestionsByInquestContent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addQuestionsByInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal QuestionsByInquestContent As QuestionsByInquestContentEntity) As Long

        ' definir los objetos
        Dim QuestionsByInquestContentDALC As New QuestionsByInquestContentDALC

        Try

            ' retornar el objeto
            addQuestionsByInquestContent = QuestionsByInquestContentDALC.add(objApplicationCredentials, QuestionsByInquestContent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addQuestionsByInquestContent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar una pregunta por contenido de encuesta.")

        Finally
            ' liberando recursos
            QuestionsByInquestContentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un QuestionsByInquestContent por el Id
    ''' </summary>
    ''' <param name="idQuestionsByInquestContent"></param>
    ''' <remarks></remarks>
    Public Function loadQuestionsByInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idQuestionsByInquestContent As Integer) As QuestionsByInquestContentEntity

        ' definir los objetos
        Dim QuestionsByInquestContentDALC As New QuestionsByInquestContentDALC

        Try

            ' retornar el objeto
            loadQuestionsByInquestContent = QuestionsByInquestContentDALC.load(objApplicationCredentials, idQuestionsByInquestContent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadQuestionsByInquestContent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una pregunta por contenido de encuesta.")

        Finally
            ' liberando recursos
            QuestionsByInquestContentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo QuestionsByInquestContent
    ''' </summary>
    ''' <param name="QuestionsByInquestContent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateQuestionsByInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal QuestionsByInquestContent As QuestionsByInquestContentEntity) As Long

        ' definir los objetos
        Dim QuestionsByInquestContentDALC As New QuestionsByInquestContentDALC

        Try

            ' retornar el objeto
            updateQuestionsByInquestContent = QuestionsByInquestContentDALC.update(objApplicationCredentials, QuestionsByInquestContent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateQuestionsByInquestContent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar una pregunta por contenido de encuesta.")

        Finally
            ' liberando recursos
            QuestionsByInquestContentDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el QuestionsByInquestContent de una forma
    ''' </summary>
    ''' <param name="idQuestionsByInquestContent"></param>
    ''' <remarks></remarks>
    Public Sub deleteQuestionsByInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idQuestionsByInquestContent As Integer)

        ' definir los objetos
        Dim QuestionsByInquestContentDALC As New QuestionsByInquestContentDALC

        Try

            ' retornar el objeto
            QuestionsByInquestContentDALC.delete(objApplicationCredentials, idQuestionsByInquestContent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteQuestionsByInquestContent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una pregunta por contenido de encuesta.")

        Finally
            ' liberando recursos
            QuestionsByInquestContentDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra todas las preguntas relacionadas con un contenido de encuesta determinado.
    ''' </summary>
    ''' <param name="idQuestionsByInquestContent">identificador del contenido de encuesta</param>
    ''' <remarks></remarks>
    Public Sub deleteAllQuestionsByInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquestContent As Integer)

        ' definir los objetos
        Dim QuestionsByInquestContentDALC As New QuestionsByInquestContentDALC

        Try

            ' retornar el objeto
            QuestionsByInquestContentDALC.deleteAllByInquestContent(objApplicationCredentials, idInquestContent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteQuestionsByInquestContent")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una pregunta por contenido de encuesta.")

        Finally
            ' liberando recursos
            QuestionsByInquestContentDALC = Nothing

        End Try

    End Sub

#End Region

#Region "AnswersByQuestion"

    ''' <summary>
    ''' Obtener la lista de AnswersByQuestion registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getAnswersByQuestionList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idquestionsbyinquestcontent As String = "", _
        Optional ByVal idinquestcontent As String = "", _
        Optional ByVal answer As String = "", _
        Optional ByVal order As String = "") As List(Of AnswersByQuestionEntity)

        ' definir los objetos
        Dim AnswersByQuestion As New AnswersByQuestionDALC

        Try

            ' retornar el objeto
            getAnswersByQuestionList = AnswersByQuestion.getList(objApplicationCredentials, _
             id, _
             idquestionsbyinquestcontent, _
             idinquestcontent, _
             answer, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getAnswersByQuestionList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de respuestas por pregunta.")

        Finally
            ' liberando recursos
            AnswersByQuestion = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo AnswersByQuestion
    ''' </summary>
    ''' <param name="AnswersByQuestion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addAnswersByQuestion(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal AnswersByQuestion As AnswersByQuestionEntity) As Long

        ' definir los objetos
        Dim AnswersByQuestionDALC As New AnswersByQuestionDALC

        Try

            ' retornar el objeto
            addAnswersByQuestion = AnswersByQuestionDALC.add(objApplicationCredentials, AnswersByQuestion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addAnswersByQuestion")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar una respuesta por pregunta.")

        Finally
            ' liberando recursos
            AnswersByQuestionDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un AnswersByQuestion por el Id
    ''' </summary>
    ''' <param name="idAnswersByQuestion"></param>
    ''' <remarks></remarks>
    Public Function loadAnswersByQuestion(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAnswersByQuestion As Integer) As AnswersByQuestionEntity

        ' definir los objetos
        Dim AnswersByQuestionDALC As New AnswersByQuestionDALC

        Try

            ' retornar el objeto
            loadAnswersByQuestion = AnswersByQuestionDALC.load(objApplicationCredentials, idAnswersByQuestion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadAnswersByQuestion")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una respuesta por pregunta.")

        Finally
            ' liberando recursos
            AnswersByQuestionDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo AnswersByQuestion
    ''' </summary>
    ''' <param name="AnswersByQuestion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateAnswersByQuestion(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal AnswersByQuestion As AnswersByQuestionEntity) As Long

        ' definir los objetos
        Dim AnswersByQuestionDALC As New AnswersByQuestionDALC

        Try

            ' retornar el objeto
            updateAnswersByQuestion = AnswersByQuestionDALC.update(objApplicationCredentials, AnswersByQuestion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateAnswersByQuestion")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar una respuesta por pregunta.")

        Finally
            ' liberando recursos
            AnswersByQuestionDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el AnswersByQuestion de una forma
    ''' </summary>
    ''' <param name="idAnswersByQuestion"></param>
    ''' <remarks></remarks>
    Public Sub deleteAnswersByQuestion(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAnswersByQuestion As Integer)

        ' definir los objetos
        Dim AnswersByQuestionDALC As New AnswersByQuestionDALC

        Try

            ' retornar el objeto
            AnswersByQuestionDALC.delete(objApplicationCredentials, idAnswersByQuestion)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteAnswersByQuestion")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una respuesta por pregunta.")

        Finally
            ' liberando recursos
            AnswersByQuestionDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Borra todas las respuestas de un contenido de encuesta determinado
    ''' </summary>
    ''' <param name="idAnswersByQuestion">identificador del contenido de la encuesta</param>
    ''' <remarks></remarks>
    Public Sub deleteAllAnswersByInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquestContent As Integer)

        ' definir los objetos
        Dim AnswersByQuestionDALC As New AnswersByQuestionDALC

        Try

            ' retornar el objeto
            AnswersByQuestionDALC.deleteAllByInquestContent(objApplicationCredentials, idInquestContent)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteAnswersByQuestion")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una respuesta por pregunta.")

        Finally
            ' liberando recursos
            AnswersByQuestionDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ResolvedInquest"

    ''' <summary>
    ''' Obtener la lista de ResolvedInquest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getResolvedInquestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idinquestcontent As String = "", _
        Optional ByVal idlikeinquestcontent As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ResolvedInquestEntity)

        ' definir los objetos
        Dim ResolvedInquest As New ResolvedInquestDALC

        Try

            ' retornar el objeto
            getResolvedInquestList = ResolvedInquest.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idinquestcontent, _
             idlikeinquestcontent, _
             comments, _
             iduser, _
             username, _
             createdate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getResolvedInquestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de encuesta resuelta.")

        Finally
            ' liberando recursos
            ResolvedInquest = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ResolvedInquest
    ''' </summary>
    ''' <param name="ResolvedInquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addResolvedInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ResolvedInquest As ResolvedInquestEntity) As Long

        ' definir los objetos
        Dim ResolvedInquestDALC As New ResolvedInquestDALC

        Try

            ' retornar el objeto
            addResolvedInquest = ResolvedInquestDALC.add(objApplicationCredentials, ResolvedInquest)

            'Se almacena la información de las respuestas digitadas
            For Each answerByResolvedInquest As AnswersByResolvedInquestEntity In ResolvedInquest.ANSWERSBYRESOLVEDINQUESTLIST
                answerByResolvedInquest.idresolvedinquest = addResolvedInquest
                'Se llama al metodo que almacena la respuesta de la pregunta actual
                Me.addAnswersByResolvedInquest(objApplicationCredentials, answerByResolvedInquest)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addResolvedInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar una encuesta resuelta.")

        Finally
            ' liberando recursos
            ResolvedInquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ResolvedInquest por el Id
    ''' </summary>
    ''' <param name="idResolvedInquest"></param>
    ''' <remarks></remarks>
    Public Function loadResolvedInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idResolvedInquest As Integer) As ResolvedInquestEntity

        ' definir los objetos
        Dim ResolvedInquestDALC As New ResolvedInquestDALC

        Try

            ' retornar el objeto
            loadResolvedInquest = ResolvedInquestDALC.load(objApplicationCredentials, idResolvedInquest)

            'Se recupera la lista de respuestas para la encuesta actual
            loadResolvedInquest.ANSWERSBYRESOLVEDINQUESTLIST = Me.getAnswersByResolvedInquestList(objApplicationCredentials, idresolvedinquest:=loadResolvedInquest.id)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadResolvedInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una encuesta resuelta.")

        Finally
            ' liberando recursos
            ResolvedInquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ResolvedInquest
    ''' </summary>
    ''' <param name="ResolvedInquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateResolvedInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ResolvedInquest As ResolvedInquestEntity) As Long

        ' definir los objetos
        Dim ResolvedInquestDALC As New ResolvedInquestDALC

        Try

            ' retornar el objeto
            updateResolvedInquest = ResolvedInquestDALC.update(objApplicationCredentials, ResolvedInquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateResolvedInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar una encuesta resuelta.")

        Finally
            ' liberando recursos
            ResolvedInquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ResolvedInquest de una forma
    ''' </summary>
    ''' <param name="idResolvedInquest"></param>
    ''' <remarks></remarks>
    Public Sub deleteResolvedInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idResolvedInquest As Integer)

        ' definir los objetos
        Dim ResolvedInquestDALC As New ResolvedInquestDALC

        Try

            ' retornar el objeto
            ResolvedInquestDALC.delete(objApplicationCredentials, idResolvedInquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteResolvedInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar una encuesta resuelta.")

        Finally
            ' liberando recursos
            ResolvedInquestDALC = Nothing

        End Try

    End Sub

#End Region

#Region "AnswersByResolvedInquest"

    ''' <summary>
    ''' Obtener la lista de AnswersByResolvedInquest registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getAnswersByResolvedInquestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idresolvedinquest As String = "", _
        Optional ByVal idquestionsbyinquestcontent As String = "", _
        Optional ByVal idanswersbyquestion As String = "", _
        Optional ByVal answertext As String = "", _
        Optional ByVal order As String = "") As List(Of AnswersByResolvedInquestEntity)

        ' definir los objetos
        Dim AnswersByResolvedInquest As New AnswersByResolvedInquestDALC

        Try

            ' retornar el objeto
            getAnswersByResolvedInquestList = AnswersByResolvedInquest.getList(objApplicationCredentials, _
             id, _
             idresolvedinquest, _
             idquestionsbyinquestcontent, _
             idanswersbyquestion, _
             answertext, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getAnswersByResolvedInquestList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de respuestas por encuesta resuelta.")

        Finally
            ' liberando recursos
            AnswersByResolvedInquest = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo AnswersByResolvedInquest
    ''' </summary>
    ''' <param name="AnswersByResolvedInquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addAnswersByResolvedInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal AnswersByResolvedInquest As AnswersByResolvedInquestEntity) As Long

        ' definir los objetos
        Dim AnswersByResolvedInquestDALC As New AnswersByResolvedInquestDALC

        Try

            ' retornar el objeto
            addAnswersByResolvedInquest = AnswersByResolvedInquestDALC.add(objApplicationCredentials, AnswersByResolvedInquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addAnswersByResolvedInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar las respuestas por encuesta resuelta.")

        Finally
            ' liberando recursos
            AnswersByResolvedInquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un AnswersByResolvedInquest por el Id
    ''' </summary>
    ''' <param name="idAnswersByResolvedInquest"></param>
    ''' <remarks></remarks>
    Public Function loadAnswersByResolvedInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAnswersByResolvedInquest As Integer) As AnswersByResolvedInquestEntity

        ' definir los objetos
        Dim AnswersByResolvedInquestDALC As New AnswersByResolvedInquestDALC

        Try

            ' retornar el objeto
            loadAnswersByResolvedInquest = AnswersByResolvedInquestDALC.load(objApplicationCredentials, idAnswersByResolvedInquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadAnswersByResolvedInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar las respuestas por encuesta resuelta.")

        Finally
            ' liberando recursos
            AnswersByResolvedInquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo AnswersByResolvedInquest
    ''' </summary>
    ''' <param name="AnswersByResolvedInquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateAnswersByResolvedInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal AnswersByResolvedInquest As AnswersByResolvedInquestEntity) As Long

        ' definir los objetos
        Dim AnswersByResolvedInquestDALC As New AnswersByResolvedInquestDALC

        Try

            ' retornar el objeto
            updateAnswersByResolvedInquest = AnswersByResolvedInquestDALC.update(objApplicationCredentials, AnswersByResolvedInquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateAnswersByResolvedInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar las respuestas por encuesta resuelta.")

        Finally
            ' liberando recursos
            AnswersByResolvedInquestDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el AnswersByResolvedInquest de una forma
    ''' </summary>
    ''' <param name="idAnswersByResolvedInquest"></param>
    ''' <remarks></remarks>
    Public Sub deleteAnswersByResolvedInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAnswersByResolvedInquest As Integer)

        ' definir los objetos
        Dim AnswersByResolvedInquestDALC As New AnswersByResolvedInquestDALC

        Try

            ' retornar el objeto
            AnswersByResolvedInquestDALC.delete(objApplicationCredentials, idAnswersByResolvedInquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteAnswersByResolvedInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar las respuestas por encuesta resuelta.")

        Finally
            ' liberando recursos
            AnswersByResolvedInquestDALC = Nothing

        End Try

    End Sub

#End Region

#Region "SubActivityMainPanel"

    Public Function getSubActivityMainPanelList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idstrategicobjective As String = "", _
        Optional ByVal strategicobjectivename As String = "", _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal StrategicLinename As String = "", _
        Optional ByVal idstrategy As String = "", _
        Optional ByVal strategyname As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal projectPhase As String = "", _
        Optional ByVal projectPhaseText As String = "", _
        Optional ByVal idcomponent As String = "", _
        Optional ByVal componentname As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal typetext As String = "", _
        Optional ByVal state As String = "", _
        Optional ByVal statetext As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal approval As String = "", _
        Optional ByVal approvalText As String = "", _
        Optional ByVal order As String = "") As List(Of SubActivityMainPanelEntity)

        Dim SubActivityMainPanel As New SubActivityMainPanelDALC

        Try

            ' retornar el objeto
            getSubActivityMainPanelList = SubActivityMainPanel.getList(objApplicationCredentials, _
            id, _
            idlike, _
            idstrategicobjective, _
            strategicobjectivename, _
            idStrategicLine, _
            StrategicLinename, _
            idstrategy, _
            strategyname, _
            idproject, _
            projectname, _
            projectPhase, _
            projectPhaseText, _
            idcomponent, _
            componentname, _
            name, _
            type, _
            typetext, _
            state, _
            statetext, _
            begindate, _
            enddate, _
            iduser, _
            username, _
            approval, _
            approvalText, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getSubActivityMainPanelList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista del panel general de subactividades (TODO-LIST).")

        Finally
            ' liberando recursos
            SubActivityMainPanel = Nothing

        End Try

    End Function

#End Region

#Region "PaymentFlow"
    Public Function loadPaymentLoad(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer) As List(Of PaymentFlowEntity)

        ' definir los objetos
        Dim PaymentFlowDALC As New PaymentFlowDALC

        Try
            ' retornar el objeto
            loadPaymentLoad = PaymentFlowDALC.loadPaymentFlowListByApproval(objApplicationCredentials, idIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Idea. ")

        Finally
            ' liberando recursos
            loadPaymentLoad = Nothing

        End Try

    End Function
#End Region

#Region "Poliza"

    ''' <summary>
    ''' TODO: Funcion Add Poliza
    ''' Funcion generada para agregar poliza
    ''' </summary>
    ''' <param name="objApplicationcredentials"></param>
    ''' <param name="poliza"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addPoliza(ByVal objApplicationcredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal poliza As PolizaEntity) As Long

        'definir los objetos
        Dim PolizaDALC As New PolizaDALC
        Dim miIdPoliza As Long

        Try
            'Se invoca el procedimiento que agregar el registro
            miIdPoliza = PolizaDALC.add(objApplicationcredentials, poliza)

            'finalizar la transaccion
            CtxSetComplete()

            Return miIdPoliza

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationcredentials.ClientName, MODULENAME, "addPoliza")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al agregar una poliza. " & ex.Message)

        Finally

            'liberando recursos
            PolizaDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Funcion generada para eliminar Poliza
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idPoliza"></param>
    ''' <remarks></remarks>
    Public Sub deletePoliza(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idPoliza As Integer)

        'definicion de objetos
        Dim PolizaDALC As New PolizaDALC

        Try

            'Se elimina la poliza actual
            PolizaDALC.delete(objApplicationCredentials, idPoliza)

            'Finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'Se verifica si la excepcion interna generada es de tipo SQLClient
            If Not ex.InnerException Is Nothing AndAlso ex.InnerException.GetType.ToString() = "System.Data.SqlClient.SqlException" Then

                Dim myException As SqlClient.SqlException = ex.InnerException

                'Verificar si el error es de integridad referencial
                If myException.Number = 547 Then

                    'cancelar la transaccion.
                    CtxSetAbort()

                    'publicar el error
                    GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deletePoliza")
                    ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                    'subir el error de nivel
                    Throw New Exception("Ha ocurrido un error al intentar eliminar este registro debido a una relacion existente.")

                End If

            Else

                'cancelar la transaccion
                CtxSetAbort()

                'publicar el error
                GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deletePoliza")
                ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                'subir el error de nivel
                Throw New Exception("Error al eliminar una Poliza. " & ex.Message)

            End If

        Finally

            'liberando recursos
            PolizaDALC = Nothing

        End Try

    End Sub

    Public Function updatePoliza(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal Poliza As PolizaEntity) As Long

        'definir los objetos
        Dim PolizaDALC As New PolizaDALC

        Try

            'se ejecuta la accion de modificacion
            updatePoliza = PolizaDALC.update(objApplicationCredentials, Poliza)

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updatePoliza")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al modificar una Poliza. " & ex.Message)


        Finally
            'liberando recursos
            PolizaDALC = Nothing

        End Try

    End Function

    Public Function loadPoliza(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idPoliza As Integer) As PolizaEntity

        'definir los objetos
        Dim PolizaDALC As New PolizaDALC

        Try

            'retornar el objeto
            loadPoliza = PolizaDALC.load(objApplicationCredentials, idPoliza)

            'finalizar la transaccion
            CtxSetAbort()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadPoliza")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al cargar una Poliza. " & ex.Message)

        Finally

            'liberando recursos
            PolizaDALC = Nothing

        End Try

    End Function

#End Region

#Region "PolizaDetails"

    ''' <summary>
    ''' TODO: Funcion Add PolizaDetails
    ''' Funcion generada para agregar Detalles de la Poliza
    ''' </summary>
    ''' <param name="objApplicationcredentials"></param>
    ''' <param name="PolizaDetails"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addPolizaDetails(ByVal objApplicationcredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal PolizaDetails As List(Of PolizaDetailsEntity), ByVal idpol As Integer) As Long

        'definir los objetos
        Dim PolizaDetailsDALC As New PolizaDetailsDALC
        Dim miIdPolizaDetails As Long

        Try

            'Se recorre la lista de conceptos de poliza
            For Each PolizaDetailsEntity In PolizaDetails
                'Se invoca el procedimiento que agrega el registro
                PolizaDetailsDALC.add(objApplicationcredentials, PolizaDetailsEntity, idpol)
            Next

            'finalizar la transaccion
            CtxSetComplete()

            Return miIdPolizaDetails

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationcredentials.ClientName, MODULENAME, "addPolizaDetails")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al agregar los detalles de la poliza. " & ex.Message)

        Finally

            'liberando recursos
            PolizaDetailsDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Funcion generada para eliminar PolizaDetails
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idPolizaDetails"></param>
    ''' <remarks></remarks>
    Public Sub deletePolizaDetails(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idPolizaDetails As Integer)

        'definicion de objetos
        Dim PolizaDetailsDALC As New PolizaDetailsDALC

        Try

            'Se elimina la PolizaDetails actual
            PolizaDetailsDALC.delete(objApplicationCredentials, idPolizaDetails)

            'Finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'Se verifica si la excepcion interna generada es de tipo SQLClient
            If Not ex.InnerException Is Nothing AndAlso ex.InnerException.GetType.ToString() = "System.Data.SqlClient.SqlException" Then

                Dim myException As SqlClient.SqlException = ex.InnerException

                'Verificar si el error es de integridad referencial
                If myException.Number = 547 Then

                    'cancelar la transaccion.
                    CtxSetAbort()

                    'publicar el error
                    GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deletePolizaDetails")
                    ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                    'subir el error de nivel
                    Throw New Exception("Ha ocurrido un error al intentar eliminar este registro debido a una relacion existente.")

                End If

            Else

                'cancelar la transaccion
                CtxSetAbort()

                'publicar el error
                GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deletePolizaDetails")
                ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                'subir el error de nivel
                Throw New Exception("Error al eliminar una PolizaDetails. " & ex.Message)

            End If

        Finally

            'liberando recursos
            PolizaDetailsDALC = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Funcion generada para actualizar detalles de la poliza
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="PolizaDetails"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updatePolizaDetails(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal PolizaDetails As PolizaDetailsEntity) As Long

        'definir los objetos
        Dim PolizaDetailsDALC As New PolizaDetailsDALC

        Try

            'se ejecuta la accion de modificacion
            updatePolizaDetails = PolizaDetailsDALC.update(objApplicationCredentials, PolizaDetails)

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updatePolizaDetails")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al modificar una PolizaDetails. " & ex.Message)


        Finally
            'liberando recursos
            PolizaDetailsDALC = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Funcion generada para leer los detalles de la poliza
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idPolizaDetails"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadPolizaDetails(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idPolizaDetails As Integer) As List(Of PolizaDetailsEntity)

        'definir los objetos
        Dim PolizaDetailsDALC As New PolizaDetailsDALC
        Dim PolizaDetailsEntity As New List(Of PolizaDetailsEntity)
        Dim polizalist As New List(Of String)

        Try

            'retornar el objeto
            'loadPolizaDetails = PolizaDetailsDALC.load(objApplicationCredentials, idPolizaDetails)

            'Se llama al metodo que pemite cargar la lista de conceptos
            PolizaDetailsEntity = Me.getPolizaDetailsList(objApplicationCredentials, idPolizaDetails)
            'contractRequestEntity.CONTRACTORLEGALENTITYBYCONTRACTREQUESTLIST = Me.getContractorLegalEntityByContractRequestList(objApplicationCredentials, idcontractrequest:=contractRequestEntity.requestnumber)

            Return PolizaDetailsEntity

            'finalizar la transaccion
            CtxSetAbort()


        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadPolizaDetails")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al cargar los detalles de una Poliza. " & ex.Message)

        Finally

            'liberando recursos
            PolizaDetailsDALC = Nothing

        End Try

    End Function

    Public Function updatePolizaId(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal PolizaId As Integer, ByVal RequestNumber As Integer) As Long

        'definir los objetos
        Dim PolizaDetailsDALC As New PolizaDetailsDALC

        Try

            'se ejecuta la accion de modificacion
            PolizaDetailsDALC.updatePolizaId(objApplicationCredentials, PolizaId, RequestNumber)

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updatePolizaDetails")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al actualizar los detalles de la poliza. " & ex.Message)


        Finally
            'liberando recursos
            PolizaDetailsDALC = Nothing

        End Try

    End Function

#End Region

#Region "CloseRegistry"

    ''' <summary>
    ''' Obtener la lista de CloseRegistry registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCloseRegistryList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal closingdate As String = "", _
        Optional ByVal weakness As String = "", _
        Optional ByVal opportunity As String = "", _
        Optional ByVal strengths As String = "", _
        Optional ByVal learningfornewprojects As String = "", _
        Optional ByVal goodpractice As String = "", _
        Optional ByVal goodpracticetext As String = "", _
        Optional ByVal registrationdate As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal order As String = "") As List(Of CloseRegistryEntity)

        ' definir los objetos
        Dim CloseRegistry As New CloseRegistryDALC

        Try

            ' retornar el objeto
            getCloseRegistryList = CloseRegistry.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idproject, _
             projectname, _
             closingdate, _
             weakness, _
             opportunity, _
             strengths, _
             learningfornewprojects, _
             goodpractice, _
             goodpracticetext, _
             registrationdate, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
   order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getCloseRegistryList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de CloseRegistry. ")

        Finally
            ' liberando recursos
            CloseRegistry = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo CloseRegistry
    ''' </summary>
    ''' <param name="CloseRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addCloseRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal CloseRegistry As CloseRegistryEntity) As Long

        ' definir los objetos
        Dim CloseRegistryDALC As New CloseRegistryDALC

        Try

            ' retornar el objeto
            addCloseRegistry = CloseRegistryDALC.add(objApplicationCredentials, CloseRegistry)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addCloseRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un CloseRegistry. ")

        Finally
            ' liberando recursos
            CloseRegistryDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un CloseRegistry por el Id
    ''' </summary>
    ''' <param name="idCloseRegistry"></param>
    ''' <remarks></remarks>
    Public Function loadCloseRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idCloseRegistry As Integer) As CloseRegistryEntity

        ' definir los objetos
        Dim CloseRegistryDALC As New CloseRegistryDALC

        Try

            ' retornar el objeto
            loadCloseRegistry = CloseRegistryDALC.load(objApplicationCredentials, idCloseRegistry)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadCloseRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un CloseRegistry. ")

        Finally
            ' liberando recursos
            CloseRegistryDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo CloseRegistry
    ''' </summary>
    ''' <param name="CloseRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateCloseRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal CloseRegistry As CloseRegistryEntity) As Long

        ' definir los objetos
        Dim CloseRegistryDALC As New CloseRegistryDALC

        Try

            ' retornar el objeto
            updateCloseRegistry = CloseRegistryDALC.update(objApplicationCredentials, CloseRegistry)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateCloseRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un CloseRegistry. ")

        Finally
            ' liberando recursos
            CloseRegistryDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el CloseRegistry de una forma
    ''' </summary>
    ''' <param name="idCloseRegistry"></param>
    ''' <remarks></remarks>
    Public Sub deleteCloseRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idCloseRegistry As Integer)

        ' definir los objetos
        Dim CloseRegistryDALC As New CloseRegistryDALC

        Try

            ' retornar el objeto
            CloseRegistryDALC.delete(objApplicationCredentials, idCloseRegistry)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteCloseRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un CloseRegistry. ")

        Finally
            ' liberando recursos
            CloseRegistryDALC = Nothing

        End Try

    End Sub

#End Region

#Region "AddContractRequest"

    Public Function loadPersonas(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
           ByVal TipoPersona As Integer) As List(Of ThirdEntity)

        ' definir los objetos
        Dim ThirdDALC As New ThirdDALC

        Try
            ' retornar el objeto
            loadPersonas = ThirdDALC.getListPersonas(objApplicationCredentials, TipoPersona)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIdea")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Idea. ")

        Finally
            ' liberando recursos
            'loadPersonas = Nothing

        End Try

    End Function

#End Region

#Region "ExecutionContractualPlanRegistry"

    ''' <summary>
    ''' Obtener la lista de ExecutionContractualPlanRegistry registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getExecutionContractualPlanRegistryList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal ProjectName As String = "", _
        Optional ByVal order As String = "") As List(Of ExecutionContractualPlanRegistryEntity)

        ' definir los objetos
        Dim ExecutionContractualPlanRegistry As New ExecutionContractualPlanRegistryDALC

        Try

            ' retornar el objeto
            getExecutionContractualPlanRegistryList = ExecutionContractualPlanRegistry.getList(objApplicationCredentials, _
             id, _
             idlike, _
             iduser, _
             username, _
             createdate, _
             ProjectName, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getExecutionContractualPlanRegistryList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ExecutionContractualPlanRegistry. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistry = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ExecutionContractualPlanRegistry
    ''' </summary>
    ''' <param name="ExecutionContractualPlanRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addExecutionContractualPlanRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ExecutionContractualPlanRegistry As ExecutionContractualPlanRegistryEntity) As Long

        ' definir los objetos
        Dim ExecutionContractualPlanRegistryDALC As New ExecutionContractualPlanRegistryDALC
        Dim ExecutionContractualPlanRegistryDetailsDALC As New ExecutionContractualPlanRegistryDetailsDALC

        Try

            ' retornar el objeto
            addExecutionContractualPlanRegistry = ExecutionContractualPlanRegistryDALC.add(objApplicationCredentials, ExecutionContractualPlanRegistry)

            'Guardar los detalles
            For Each ecpd As ExecutionContractualPlanRegistryDetailsEntity In ExecutionContractualPlanRegistry.ExecutionContractualPlanRegistryEntityDetails
                ecpd.idexecutioncontractualplanregistry = addExecutionContractualPlanRegistry
                ExecutionContractualPlanRegistryDetailsDALC.add(objApplicationCredentials, ecpd)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addExecutionContractualPlanRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ExecutionContractualPlanRegistry. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistryDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ExecutionContractualPlanRegistry por el Id
    ''' </summary>
    ''' <param name="idExecutionContractualPlanRegistry"></param>
    ''' <remarks></remarks>
    Public Function loadExecutionContractualPlanRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecutionContractualPlanRegistry As Integer) As ExecutionContractualPlanRegistryEntity

        ' definir los objetos
        Dim ExecutionContractualPlanRegistryDALC As New ExecutionContractualPlanRegistryDALC

        Try

            ' retornar el objeto
            loadExecutionContractualPlanRegistry = ExecutionContractualPlanRegistryDALC.load(objApplicationCredentials, idExecutionContractualPlanRegistry)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadExecutionContractualPlanRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ExecutionContractualPlanRegistry. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistryDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ExecutionContractualPlanRegistry
    ''' </summary>
    ''' <param name="ExecutionContractualPlanRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateExecutionContractualPlanRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ExecutionContractualPlanRegistry As ExecutionContractualPlanRegistryEntity) As Long

        ' definir los objetos
        Dim ExecutionContractualPlanRegistryDALC As New ExecutionContractualPlanRegistryDALC
        Dim ExecutionContractualPlanRegistryDetailsDALC As New ExecutionContractualPlanRegistryDetailsDALC

        Try

            'Rutina modificada por Jose Olmes Torres J. - Junio 23 de 2010
            'Se pone en conmentarios la siguiente rutina debido a que el único campo editable (Comentarios)
            ' de esta tabla fue movido a la tabla ExecutionContractualPlanRegistryDetails.
            ' retornar el objeto
            'updateExecutionContractualPlanRegistry = ExecutionContractualPlanRegistryDALC.update(objApplicationCredentials, ExecutionContractualPlanRegistry)

            ' Actualizar los detalles
            ExecutionContractualPlanRegistryDetailsDALC.delete(objApplicationCredentials, 0, ExecutionContractualPlanRegistry.id)
            For Each ecpd As ExecutionContractualPlanRegistryDetailsEntity In ExecutionContractualPlanRegistry.ExecutionContractualPlanRegistryEntityDetails
                ecpd.idexecutioncontractualplanregistry = ExecutionContractualPlanRegistry.id
                ExecutionContractualPlanRegistryDetailsDALC.add(objApplicationCredentials, ecpd)
            Next

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateExecutionContractualPlanRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ExecutionContractualPlanRegistry. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistryDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ExecutionContractualPlanRegistry de una forma
    ''' </summary>
    ''' <param name="idExecutionContractualPlanRegistry"></param>
    ''' <remarks></remarks>
    Public Sub deleteExecutionContractualPlanRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecutionContractualPlanRegistry As Integer)

        ' definir los objetos
        Dim ExecutionContractualPlanRegistryDALC As New ExecutionContractualPlanRegistryDALC

        Try

            ' retornar el objeto
            ExecutionContractualPlanRegistryDALC.delete(objApplicationCredentials, idExecutionContractualPlanRegistry)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteExecutionContractualPlanRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ExecutionContractualPlanRegistry. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistryDALC = Nothing

        End Try

    End Sub

#End Region

#Region "ExecutionContractualPlanRegistryDetails"

    ''' <summary>
    ''' Obtener la lista de ExecutionContractualPlanRegistryDetails registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getExecutionContractualPlanRegistryDetailsList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idexecutioncontractualplanregistry As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal concept As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal typetext As String = "", _
        Optional ByVal totalcost As String = "", _
        Optional ByVal engagementdate As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ExecutionContractualPlanRegistryDetailsEntity)

        ' definir los objetos
        Dim ExecutionContractualPlanRegistryDetails As New ExecutionContractualPlanRegistryDetailsDALC

        Try

            ' retornar el objeto
            getExecutionContractualPlanRegistryDetailsList = ExecutionContractualPlanRegistryDetails.getList(objApplicationCredentials, _
             id, _
             idlike, _
             idexecutioncontractualplanregistry, _
             idproject, _
             projectname, _
             concept, _
             type, _
             typetext, _
             totalcost, _
             engagementdate, _
             comments, _
             createdate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getExecutionContractualPlanRegistryDetailsList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ExecutionContractualPlanRegistryDetails. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistryDetails = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ExecutionContractualPlanRegistryDetails
    ''' </summary>
    ''' <param name="ExecutionContractualPlanRegistryDetails"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addExecutionContractualPlanRegistryDetails(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ExecutionContractualPlanRegistryDetails As ExecutionContractualPlanRegistryDetailsEntity) As Long

        ' definir los objetos
        Dim ExecutionContractualPlanRegistryDetailsDALC As New ExecutionContractualPlanRegistryDetailsDALC

        Try

            ' retornar el objeto
            addExecutionContractualPlanRegistryDetails = ExecutionContractualPlanRegistryDetailsDALC.add(objApplicationCredentials, ExecutionContractualPlanRegistryDetails)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addExecutionContractualPlanRegistryDetails")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un ExecutionContractualPlanRegistryDetails. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistryDetailsDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ExecutionContractualPlanRegistryDetails por el Id
    ''' </summary>
    ''' <param name="idExecutionContractualPlanRegistryDetails"></param>
    ''' <remarks></remarks>
    Public Function loadExecutionContractualPlanRegistryDetails(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecutionContractualPlanRegistryDetails As Integer) As ExecutionContractualPlanRegistryDetailsEntity

        ' definir los objetos
        Dim ExecutionContractualPlanRegistryDetailsDALC As New ExecutionContractualPlanRegistryDetailsDALC

        Try

            ' retornar el objeto
            loadExecutionContractualPlanRegistryDetails = ExecutionContractualPlanRegistryDetailsDALC.load(objApplicationCredentials, idExecutionContractualPlanRegistryDetails)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadExecutionContractualPlanRegistryDetails")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ExecutionContractualPlanRegistryDetails. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistryDetailsDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ExecutionContractualPlanRegistryDetails
    ''' </summary>
    ''' <param name="ExecutionContractualPlanRegistryDetails"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateExecutionContractualPlanRegistryDetails(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ExecutionContractualPlanRegistryDetails As ExecutionContractualPlanRegistryDetailsEntity) As Long

        ' definir los objetos
        Dim ExecutionContractualPlanRegistryDetailsDALC As New ExecutionContractualPlanRegistryDetailsDALC

        Try

            ' retornar el objeto
            updateExecutionContractualPlanRegistryDetails = ExecutionContractualPlanRegistryDetailsDALC.update(objApplicationCredentials, ExecutionContractualPlanRegistryDetails)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateExecutionContractualPlanRegistryDetails")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar un ExecutionContractualPlanRegistryDetails. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistryDetailsDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ExecutionContractualPlanRegistryDetails de una forma
    ''' </summary>
    ''' <param name="idExecutionContractualPlanRegistryDetails"></param>
    ''' <remarks></remarks>
    Public Sub deleteExecutionContractualPlanRegistryDetails(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecutionContractualPlanRegistryDetails As Integer, _
       Optional ByVal idExecutionContractualPlanRegistry As Integer = 0)

        ' definir los objetos
        Dim ExecutionContractualPlanRegistryDetailsDALC As New ExecutionContractualPlanRegistryDetailsDALC

        Try

            ' retornar el objeto
            ExecutionContractualPlanRegistryDetailsDALC.delete(objApplicationCredentials, idExecutionContractualPlanRegistryDetails, idExecutionContractualPlanRegistry)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteExecutionContractualPlanRegistryDetails")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un ExecutionContractualPlanRegistryDetails. ")

        Finally
            ' liberando recursos
            ExecutionContractualPlanRegistryDetailsDALC = Nothing

        End Try

    End Sub

#Region "ContractType"

    ''' <summary>
    ''' Obtener la lista de ContractType registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getContractTypeList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ContractTypeEntity)

        ' definir los objetos
        Dim ContractType As New ContractTypeDALC

        Try

            ' retornar el objeto
            getContractTypeList = ContractType.getList(objApplicationCredentials, _
             id, _
             name, _
             enabled, _
             iduser, _
             createdate, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getContractTypeList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de tipos de contrato.")

        Finally
            ' liberando recursos
            ContractType = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ContractType
    ''' </summary>
    ''' <param name="ContractType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addContractType(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractType As ContractTypeEntity) As Long

        ' definir los objetos
        Dim ContractTypeDALC As New ContractTypeDALC

        Try

            ' retornar el objeto
            addContractType = ContractTypeDALC.add(objApplicationCredentials, ContractType)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addContractType")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar un tipo de contrato.")

        Finally
            ' liberando recursos
            ContractTypeDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractType por el Id
    ''' </summary>
    ''' <param name="idContractType"></param>
    ''' <remarks></remarks>
    Public Function loadContractType(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractType As Integer) As ContractTypeEntity

        ' definir los objetos
        Dim ContractTypeDALC As New ContractTypeDALC

        Try

            ' retornar el objeto
            loadContractType = ContractTypeDALC.load(objApplicationCredentials, idContractType)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadContractType")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un tipo de contrato.")

        Finally
            ' liberando recursos
            ContractTypeDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractType
    ''' </summary>
    ''' <param name="ContractType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateContractType(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractType As ContractTypeEntity) As Long

        ' definir los objetos
        Dim ContractTypeDALC As New ContractTypeDALC

        Try

            ' retornar el objeto
            updateContractType = ContractTypeDALC.update(objApplicationCredentials, ContractType)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateContractType")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar tipo de contrato.")

        Finally
            ' liberando recursos
            ContractTypeDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractType de una forma
    ''' </summary>
    ''' <param name="idContractType"></param>
    ''' <remarks></remarks>
    Public Sub deleteContractType(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractType As Integer)

        ' definir los objetos
        Dim ContractTypeDALC As New ContractTypeDALC

        Try

            ' retornar el objeto
            ContractTypeDALC.delete(objApplicationCredentials, idContractType)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteContractType")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un tipo de contrato.")

        Finally
            ' liberando recursos
            ContractTypeDALC = Nothing

        End Try

    End Sub

#End Region

#End Region

#Region "ProjectPhase"

    ''' <summary>
    ''' Obtener la lista de ProjectPhase registradas en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProjectPhaseList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal isenabled As String = "", _
        Optional ByVal order As String = "") As List(Of ProjectPhaseEntity)

        ' definir los objetos
        Dim ProjectPhase As New ProjectPhaseDALC

        Try

            ' retornar el objeto
            getProjectPhaseList = ProjectPhase.getList(objApplicationCredentials, _
             id, _
             code, _
             name, _
             isenabled, _
            order)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProjectPhaseList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de fases de un proyecto.")

        Finally
            ' liberando recursos
            ProjectPhase = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Registar un nuevo ProjectPhase
    ''' </summary>
    ''' <param name="ProjectPhase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addProjectPhase(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProjectPhase As ProjectPhaseEntity) As Long

        ' definir los objetos
        Dim ProjectPhaseDALC As New ProjectPhaseDALC

        Try

            ' retornar el objeto
            addProjectPhase = ProjectPhaseDALC.add(objApplicationCredentials, ProjectPhase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "addProjectPhase")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al agregar una fase de un proyecto.")

        Finally
            ' liberando recursos
            ProjectPhaseDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProjectPhase por el Id
    ''' </summary>
    ''' <param name="idProjectPhase"></param>
    ''' <remarks></remarks>
    Public Function loadProjectPhase(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProjectPhase As Integer) As ProjectPhaseEntity

        ' definir los objetos
        Dim ProjectPhaseDALC As New ProjectPhaseDALC

        Try

            ' retornar el objeto
            loadProjectPhase = ProjectPhaseDALC.load(objApplicationCredentials, idProjectPhase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadProjectPhase")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar fase de un proyecto.")

        Finally
            ' liberando recursos
            ProjectPhaseDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProjectPhase
    ''' </summary>
    ''' <param name="ProjectPhase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateProjectPhase(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProjectPhase As ProjectPhaseEntity) As Long

        ' definir los objetos
        Dim ProjectPhaseDALC As New ProjectPhaseDALC

        Try

            ' retornar el objeto
            updateProjectPhase = ProjectPhaseDALC.update(objApplicationCredentials, ProjectPhase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateProjectPhase")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar fase de un proyecto.")

        Finally
            ' liberando recursos
            ProjectPhaseDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProjectPhase de una forma
    ''' </summary>
    ''' <param name="idProjectPhase"></param>
    ''' <remarks></remarks>
    Public Sub deleteProjectPhase(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProjectPhase As Integer)

        ' definir los objetos
        Dim ProjectPhaseDALC As New ProjectPhaseDALC

        Try

            ' retornar el objeto
            ProjectPhaseDALC.delete(objApplicationCredentials, idProjectPhase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "deleteProjectPhase")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar fase de un proyecto.")

        Finally
            ' liberando recursos
            ProjectPhaseDALC = Nothing

        End Try

    End Sub

#End Region

End Class

