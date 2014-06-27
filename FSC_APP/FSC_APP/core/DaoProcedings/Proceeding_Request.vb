Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports System.Data.SqlClient
Imports Gattaca.Application.ExceptionManager
Imports System.IO
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity

Partial Public Class Proceeding_Request

#Region "Campos TOR Request"

    'Encabezado TOR
    Private _type_request As String
    Private _strategic_line As String
    Private _contract_nature As String
    Private _contract_number As String
    Private _subscription_year As String
    Private _number_request As String
    Private _date_request As String

    'Parte1
    Private _justification As String

    'Parte 2 - 1 Alcance
    Private _scope As String = "{9}"

    'Parte3
    Private _risks As String = "{14}"

    'Campos sistema
    Private _directorioActas As String
    Private _idproject As String

    'Generar el código HTML
    'Encabezado
    Private _proceedingHeader_html As String = "<html><meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />"

    'Parte 0 - Parte inicial
    Private _proceedingzero_html As String = "<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td colspan=""2"" style=""text-align: center; vertical-align: middle;""><strong>SOLICITUD MODIFICACIÓN CONTRACTUAL</strong></td></tr><tr><td colspan=""2""></td></tr><tr><td colspan=""2""><strong>TIPO DE SOLICITUD:</strong></td></tr><tr><td colspan=""2"">{1}</td></tr><tr><td style=""width: 33%;""><strong>Línea Estratégica:</strong></td><td>{2}</td></tr><tr><td><strong>No. del Proyecto:</strong></td><td>{0}</td></tr><tr><td><strong>Naturaleza de Contratación:</strong></td><td>{4}</td></tr><tr><td><strong>No. Del Contrato/Convenio/OPS:</strong></td><td>{3}</td></tr><tr><td><strong>Año de Suscripción del Contrato/Convenio/OPS:</strong></td><td>{5}</td></tr><tr><td><strong>Otrosí No. :</strong></td><td>{6}</td></tr><tr><td><strong>Fecha de Solicitud:</strong></td><td>{7}</td></tr></tbody></table>"

    'Parte 1 - Justificación
    Private _proceedingone_html As String = "<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td><strong>1. JUSTIFICACIÓN DE LA SOLICITUD</strong></td></tr><tr><td>{8}</td></tr></tbody></table>"

    'Encabezado Parte 2
    Private _part2header As String = "<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td>&nbsp;</td></tr><tr><td><strong>2. DETALLE DE LO QUE SE MODIFICA</strong></td></tr><tr><td>&nbsp;</td></tr></tbody></table>"

    'Parte 2 - Detalles

    'Parte 2 - 2 Suspension
    Private _suspension As String = "{10}"

    'Parte 2 - 3 Adicion
    Private _adition As String = ""

    'Parte 2 - 4 Cesion
    Private _cesion As String = ""

    'Parte 2 - 5 Otros
    Private _others As String = "{13}"

    'Consolidar parte 2
    Private _proceedingtwo_hmtl As String = _scope & _suspension & _adition & _cesion & _others

    'Cierre HTML
    Private _proceedingFooter_html As String = "</html>"

    'Consolidar
    Private _proceedingStar_html As String = _proceedingHeader_html & _proceedingzero_html & _proceedingone_html & _part2header & _proceedingtwo_hmtl & _risks & _proceedingFooter_html

#End Region

#Region "Propiedades TOR Request"

    Public Property Justification() As String
        Get
            Return Me._justification
        End Get
        Set(ByVal value As String)
            Me._justification = value
        End Set
    End Property

    Public Property idProject() As String
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As String)
            Me._idproject = value
        End Set
    End Property

    Public Property directorio_Actas() As String
        Get
            Return Me._directorioActas
        End Get
        Set(ByVal value As String)
            Me._directorioActas = value
        End Set
    End Property

    Public Property type_request() As String
        Get
            Return Me._type_request
        End Get
        Set(ByVal value As String)
            Me._type_request = value
        End Set
    End Property

    Public Property strategic_line() As String
        Get
            Return Me._strategic_line
        End Get
        Set(ByVal value As String)
            Me._strategic_line = value
        End Set
    End Property

    Public Property contract_nature() As String
        Get
            Return Me._contract_nature
        End Get
        Set(ByVal value As String)
            Me._contract_nature = value
        End Set
    End Property

    Public Property contract_number() As String
        Get
            Return Me._contract_number
        End Get
        Set(ByVal value As String)
            Me._contract_number = value
        End Set
    End Property

    Public Property subscription_year() As String
        Get
            Return Me._subscription_year
        End Get
        Set(ByVal value As String)
            Me._subscription_year = value
        End Set
    End Property

    Public Property number_request() As String
        Get
            Return Me._number_request
        End Get
        Set(ByVal value As String)
            Me._number_request = value
        End Set
    End Property

    Public Property date_request() As String
        Get
            Return Me._date_request
        End Get
        Set(ByVal value As String)
            Me._date_request = value
        End Set
    End Property

    Public Property scope() As String
        Get
            Return Me._scope
        End Get
        Set(ByVal value As String)
            Me._scope = value
        End Set
    End Property

    Public Property suspension() As String
        Get
            Return Me._suspension
        End Get
        Set(ByVal value As String)
            Me._suspension = value
        End Set
    End Property

    Public Property adition() As String
        Get
            Return Me._adition
        End Get
        Set(ByVal value As String)
            Me._adition = value
        End Set
    End Property

    Public Property cesion() As String
        Get
            Return Me._cesion
        End Get
        Set(ByVal value As String)
            Me._cesion = value
        End Set
    End Property

    Public Property others() As String
        Get
            Return Me._others
        End Get
        Set(ByVal value As String)
            Me._others = value
        End Set
    End Property

    Public Property risks() As String
        Get
            Return Me._risks
        End Get
        Set(ByVal value As String)
            Me._risks = value
        End Set
    End Property

#End Region

#Region "funciones"

    Function WriteFile(ByVal Ruta As String, ByVal html As String) As Boolean
        Try
            Dim objStream As Stream = System.IO.File.Open(Ruta, FileMode.Append, FileAccess.Write, FileShare.None)
            Using objStreamWriter As StreamWriter = New StreamWriter(objStream)
                objStreamWriter.AutoFlush = True
                objStreamWriter.Write(html)
                objStreamWriter.Close()
            End Using
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Function ExportProceedingsStart() As String

        Try
            'instanciamos funcion que llama el html
            Dim nameFile As String = String.Format("/Proceedings/Terminos_de_Referencia_{1}_{2}.doc", _directorioActas, _idproject, Convert.ToDateTime(DateTime.Now).ToString("yyyy_MM_dd_hh_mm_ss"))
            Dim fullPath As String = String.Format("{0}{1}", _directorioActas, nameFile)

            _proceedingStar_html = String.Format(_proceedingStar_html, _idproject, _type_request, _strategic_line, _contract_number, _contract_nature, _subscription_year, _number_request, _date_request, _justification, _scope, _suspension, "11", "12", _others, _risks)
            WriteFile(fullPath, _proceedingStar_html)

            Return nameFile

        Catch ex As Exception
            Return ""

        End Try

    End Function

#End Region

End Class
