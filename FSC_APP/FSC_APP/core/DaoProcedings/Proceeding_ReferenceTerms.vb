﻿Imports System.IO
Imports Microsoft.VisualBasic

Public Class Proceedings

#Region "terminos de referencia"

    Private _Linea_Estrategica_t As String
    Private _Programa_t As String
    Private _name_idea_proyecto_t As String
    Private _location_t As String
    Private _people_benefactor_t As String
    Private _duration_t As String
    Private _date_inicial_t As String
    Private _values_total_t As String
    Private _modalidad_contratos_t As String
    Private _id_idea_proyecto_t As String
    Private _code_idea_proyecto_t As String
    Private _justificacion_t As String
    Private _objetive_t As String
    Private _objetive_esp_t As String
    Private _result_be_t As String
    Private _result_gest_t As String
    Private _capacidad_ins_t As String
    Private _value_total_t As String
    Private _aport_socios_t As String
    Private _aport_FSC_t As String
    Private _flujos_t As String
    Private _tflujos_t As String


    'grid actores
    Private _actors_t As String
    Private _tipo_t As String
    Private _contact_t As String
    Private _document_t As String
    Private _phone_t As String
    Private _email_t As String

    'creamos el html seguido por formato enviado por FSC 
    Private _Termsofreference_html As String = "<html><meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /><table  style=""font-family: 'Times New Roman';"" Width=""100%""><body><p style=""text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">T&Eacute;RMINOS DE REFERENCIA</span></strong></p><p><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span></p>        <table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>L&iacute;nea Estrat&eacute;gica:</strong></span></td><td>{0} </td></tr>        <tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Programa:</strong></span></td><td>{1} </td></tr>        <tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Nombre del proyecto :</strong></span></td><td>{2} </td></tr>        <tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>No del Proyecto:</strong></span></td><td>{3} </td></tr>        <tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Localizaci&oacute;n Geogr&aacute;fica:</strong></span></td><td>        {4}        </td></tr><tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Poblaci&oacute;n Beneficiaria:</strong></span></td><td>{5} </td></tr>        <tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Duraci&oacute;n en meses:</strong></span></td><td>{6} </td></tr>        <tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha de Inicio:</strong></span></td><td>{7} </td></tr>        <tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor Total:</strong></span></td><td>{8}  </td></tr>        <tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Modalidad de contrataci&oacute;n:</strong></span></span></td><td>{9} </td></tr>        <tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Nombre de la idea:</strong></span></td><td>{10} </td></tr></tbody></table>        <p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actores:</strong></span></p>        <table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actor</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tipo</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contacto</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Documento de Identidad</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tel&eacute;fono</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Correo electr&oacute;nico</strong></span></td></tr><tr><td>{11}</td></tr>       </tbody></table>		 		<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">JUSTIFICAC&Iacute;ON:</span></strong></p>        <table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""67"" width=""100%""><tbody><tr><td style=""text-align: justify;"">{12} </td></tr></tbody></table>        <p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVO:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""71"" width=""100%""><tbody><tr><td style=""text-align: justify;"">{13} </td></tr></tbody></table>        <p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVOS ESPEC&Iacute;FICOS:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""82"" style=""width: 100%;"" width=""100%""><tbody><tr><td style=""text-align: justify;"">{14}</td></tr></tbody></table>        <p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">RESULTADOS ESPERADOS:</span></strong></p>        <table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr>        <td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Beneficiarios:</span></strong></td><td style=""text-align: justify;"">{15} </td></tr><tr>        <td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Gesti&oacute;n del conocimiento*:</span></strong></td><td style=""text-align: justify;"">{16} </td></tr><tr>        <td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Capacidad instalada:</span></strong></td><td style=""text-align: justify;"">{17} </td></tr></tbody></table>        <p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">PRESUPUESTO GENERAL:</span></strong></p>        <table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr>        <td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Valor Total del contrato:</span></strong></td><td>{18} </td></tr>        <tr><td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Aporte de los Socios (Efectivo y Especie):</span></strong></td><td>{19} </td></tr>        <tr><td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Aporte de la FSC (Efectivo y Especie):</span></strong></td><td>{20} </td></tr></tbody></table>        <span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span>        <p><u><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">CRONOGRAMA DE PAGOS</span></strong></u></p>        <table border=""1"" cellpadding=""1"" cellspacing=""1"" width=""100%""><tbody><tr><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor</strong></span></td><td style=""width: 5%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>%</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Origen de los Recursos</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contraentrega</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha</strong></span></td></tr><tr><td>{21}</td></tr><tr><td>{22}</td></tr>       </tbody></table>		        		<p>&nbsp;</p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong><u>IDENTIFICACI&Oacute;N DE RIESGOS</u></strong></span></p><p>&nbsp;</p>        <table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 50%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Riesgo identificado</strong></span></td><td style=""text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Acci&oacute;n de mitigaci&oacute;n</strong></span></td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr></tbody></table>        <p><strong>*Nota:&nbsp; </strong>En la Fundaci&oacute;n Saldarriaga Concha promovemos la cultura de racionalizaci&oacute;n en el uso del papel, por lo que se solicita informar a nuestros operadores que solo debe enviar el <strong>informe final </strong>impreso<strong>.</strong></p></html>"

#End Region

#Region "Propiedades de terminos de referencia"

    Public Property Linea_Estrategica_t() As String
        Get
            Return Me._Linea_Estrategica_t
        End Get
        Set(ByVal value As String)
            Me._Linea_Estrategica_t = value
        End Set
    End Property
    Public Property Programa_t() As String
        Get
            Return Me._Programa_t
        End Get
        Set(ByVal value As String)
            Me._Programa_t = value
        End Set
    End Property
    Public Property name_idea_proyecto_t() As String
        Get
            Return Me._name_idea_proyecto_t
        End Get
        Set(ByVal value As String)
            Me._name_idea_proyecto_t = value
        End Set
    End Property

    Public Property code_idea_proyecto_t() As String
        Get
            Return Me._code_idea_proyecto_t
        End Get
        Set(ByVal value As String)
            Me._code_idea_proyecto_t = value
        End Set
    End Property

    Public Property location_t() As String
        Get
            Return Me._location_t
        End Get
        Set(ByVal value As String)
            Me._location_t = value
        End Set
    End Property
    Public Property people_benefactor_t() As String
        Get
            Return Me._people_benefactor_t
        End Get
        Set(ByVal value As String)
            Me._people_benefactor_t = value
        End Set
    End Property
    Public Property duration_t() As String
        Get
            Return Me._duration_t
        End Get
        Set(ByVal value As String)
            Me._duration_t = value
        End Set
    End Property
    Public Property date_inicial_t() As String
        Get
            Return Me._date_inicial_t
        End Get
        Set(ByVal value As String)
            Me._date_inicial_t = value
        End Set
    End Property
    Public Property values_total_t() As String
        Get
            Return Me._values_total_t
        End Get
        Set(ByVal value As String)
            Me._values_total_t = value
        End Set
    End Property
    Public Property modalidad_contratos_t() As String
        Get
            Return Me._modalidad_contratos_t
        End Get
        Set(ByVal value As String)
            Me._modalidad_contratos_t = value
        End Set
    End Property
    Public Property id_idea_proyecto_t() As String
        Get
            Return Me._id_idea_proyecto_t
        End Get
        Set(ByVal value As String)
            Me._id_idea_proyecto_t = value
        End Set
    End Property
    Public Property justificacion_t() As String
        Get
            Return Me._justificacion_t
        End Get
        Set(ByVal value As String)
            Me._justificacion_t = value
        End Set
    End Property
    Public Property objetive_t() As String
        Get
            Return Me._objetive_t
        End Get
        Set(ByVal value As String)
            Me._objetive_t = value
        End Set
    End Property
    Public Property objetive_esp_t() As String
        Get
            Return Me._objetive_esp_t
        End Get
        Set(ByVal value As String)
            Me._objetive_esp_t = value
        End Set
    End Property
    Public Property result_be_t() As String
        Get
            Return Me._result_be_t
        End Get
        Set(ByVal value As String)
            Me._result_be_t = value
        End Set
    End Property
    Public Property result_gest_t() As String
        Get
            Return Me._result_gest_t
        End Get
        Set(ByVal value As String)
            Me._result_gest_t = value
        End Set
    End Property
    Public Property capacidad_ins_t() As String
        Get
            Return Me._capacidad_ins_t
        End Get
        Set(ByVal value As String)
            Me._capacidad_ins_t = value
        End Set
    End Property
    Public Property value_total_t() As String
        Get
            Return Me._value_total_t
        End Get
        Set(ByVal value As String)
            Me._value_total_t = value
        End Set
    End Property
    Public Property aport_socios_t() As String
        Get
            Return Me._aport_socios_t
        End Get
        Set(ByVal value As String)
            Me._aport_socios_t = value
        End Set
    End Property
    Public Property aport_FSC_t() As String
        Get
            Return Me._aport_FSC_t
        End Get
        Set(ByVal value As String)
            Me._aport_FSC_t = value
        End Set
    End Property
    Public Property actors_t() As String
        Get
            Return Me._actors_t
        End Get
        Set(ByVal value As String)
            Me._actors_t = value
        End Set
    End Property
    Public Property tipo_t() As String
        Get
            Return Me._tipo_t
        End Get
        Set(ByVal value As String)
            Me._tipo_t = value
        End Set
    End Property
    Public Property contact_t() As String
        Get
            Return Me._contact_t
        End Get
        Set(ByVal value As String)
            Me._contact_t = value
        End Set
    End Property
    Public Property document_t() As String
        Get
            Return Me._document_t
        End Get
        Set(ByVal value As String)
            Me._document_t = value
        End Set
    End Property
    Public Property phone_t() As String
        Get
            Return Me._phone_t
        End Get
        Set(ByVal value As String)
            Me._phone_t = value
        End Set
    End Property
    Public Property email_t() As String
        Get
            Return Me._email_t
        End Get
        Set(ByVal value As String)
            Me._email_t = value
        End Set
    End Property
    Public Property flujos_t() As String
        Get
            Return Me._flujos_t
        End Get
        Set(ByVal value As String)
            Me._flujos_t = value
        End Set
    End Property
    Public Property tflujos_t() As String
        Get
            Return Me._tflujos_t
        End Get
        Set(ByVal value As String)
            Me._tflujos_t = value
        End Set
    End Property


#End Region

#Region "Funciones terminos de referencia"


    Function ExportReferenceTerms() As String

        Try
            'instanciamos funcion que llama el html
            Dim nameFile As String = String.Format("/Proceedings/Proyect_ContractExport_{1}_{2}.doc", _directorioActas, _Idproject, Convert.ToDateTime(DateTime.Now).ToString("yyyy_MM_dd_hh_mm_ss"))
            Dim fullPath As String = String.Format("{0}{1}", _directorioActas, nameFile)

            _Termsofreference_html = String.Format(_Termsofreference_html, _Linea_Estrategica_t, _Programa_t, _name_idea_proyecto_t, _id_idea_proyecto_t, _location_t, _people_benefactor_t, _duration_t, _date_inicial_t, _values_total_t, _modalidad_contratos_t, code_idea_proyecto_t, _actors_t, _justificacion_t, _objetive_t, _objetive_esp_t, _result_be_t, _result_gest_t, _capacidad_ins_t, _values_total_t, _aport_socios_t, _aport_FSC_t, _flujos_t, _tflujos_t)

            WriteFile(fullPath, _Termsofreference_html)

            Return nameFile
        Catch ex As Exception
            Return ""
        End Try

    End Function

#End Region

End Class
