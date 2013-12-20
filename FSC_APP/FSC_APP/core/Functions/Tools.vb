Imports System.EnterpriseServices
Imports Microsoft.VisualBasic

Module Tools

    ''' <summary>
    ''' Cancelar (RollBack) la transaccion en curso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <modelId>3C3C9CA9032B</modelId>
    Public Sub CtxSetAbort()

        ' verificar si hay contexto de transaccion
        If ContextUtil.IsInTransaction Then

            ' rollback de los cambios
            ContextUtil.SetAbort()

        End If

    End Sub

    ''' <summary>
    ''' Confirmar (Commit) la transaccion en curso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <modelId>3C3C9CA90367</modelId>
    Public Sub CtxSetComplete()

        ' verificar si hay contexto de tranaccion
        If ContextUtil.IsInTransaction Then

            ' commit de los cambios
            ContextUtil.SetComplete()

        End If

    End Sub


    ''' <summary>
    ''' Vericar si existe thema para el cliente
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub checkTheme(ByVal page As System.Web.UI.Page)

        If HttpContext.Current.Session("Theme") IsNot Nothing Then

            ' quemar el tema del cliente
            page.Theme = HttpContext.Current.Session("Theme").ToString

        Else
            ' quemar el tema por defecto
            page.Theme = "GattacaAdmin"

        End If

    End Sub

End Module
