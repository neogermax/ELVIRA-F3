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

Partial Public Class Proceedings

#Region "Campos acta de seguimiento"

    Private _ProceedVersion As String
    Private _ProceedCode As String
    Private _ProceedApprovalDate As String
    Private _ProceedNumberOfPages As String
    Private _RecordNumber As String
    Private _SignedRecord As String
    Private _ComitteeDate As String
    Private _ArchivedRecord As String
    Private _PartnerOperatorName As String
    Private _ContractNumber As String
    Private _ContractStartDate As String
    Private _ContractFinishDate As String
    Private _ContractObject As String
    Private _ContractValue As String
    Private _ContractValueLetters As String
    Private _Aditions As String
    Private _NumberOfAdditions As String
    Private _AdditionValueLetters As String
    Private _AdditionDate As String
    Private _AdditionValue As String
    Private _ContractFinalValue As String
    Private _AdditionInLetters As String
    Private _Assistant1 As String
    Private _Assistant2 As String
    Private _Assistant3 As String
    Private _Assistant4 As String
    Private _Assistant5 As String
    Private _Assistant6 As String
    Private _Assistant7 As String
    Private _DayOrder1 As String
    Private _DayOrder2 As String
    Private _DayOrder3 As String
    Private _Liability1 As String
    Private _Liability2 As String
    Private _Liability3 As String
    Private _Liable1 As String
    Private _Liable2 As String
    Private _Liable3 As String
    Private _TracingDate1 As String
    Private _TracingDate2 As String
    Private _TracingDate3 As String
    Private _COMPROMISELIST As List(Of CompromiseEntity)
    Private _TABLECOMPROMISE As String

    'Labels
    Private _lblTypeThird As String
    Private _lblDatesTrace As String
    Private _lblObject As String
    Private _lblValue As String
    Private _lblAdditions As String
    Private _Label1 As String
    Private _lblProceedNumber As String

    Private _Tracing_Html As String = "<html><meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /><table cellpadding=""1"" cellspacing=""1"" id=""Acta_Seguimiento"" style=""width: 80%;"" align=""center""><tr><td style=""border: solid 1px; border-color: #000000; width: 33%;"" colspan=""2"" rowspan=""1""><img id=""ctl00_headerLeft"" src=""../App_Themes/GattacaAdmin/Images/Template/header_leftBPO.jpg""style=""border-width: 0px;""></td><td colspan=""4"" style=""text-align: center; border: 1px solid #000000; font-size: large;""bgcolor=""#F2F2F2""><b> ACTA DE COMITÉ </b></td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr> <td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2"">  <b>Acta No.:</b> </td> <td style=""border: solid 1px; border-color: #000000;"">  {31} </td> <td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2"">  <b>Fecha del comité:</b> </td> <td style=""border: solid 1px; border-color: #000000;"" colspan=""3"">  {32} </td></tr><tr> <td colspan=""6"" rowspan=""1"" style=""text-align: center;"">  <p>  </p> </td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""1"" bgcolor=""#F2F2F2""><b>{33}</b></td><td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2"" colspan=""3""><b>{39}</b></td><td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{34}</b></td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""2"">{0}</td><td style=""border: solid 1px; border-color: #000000;"" rowspan=""2"" colspan=""3"">{1}</td><td style=""border: solid 1px; border-color: #000000;""><b>Desde: </b>{2}</td></tr><tr><td style=""border: solid 1px; border-color: #000000;""><b>Hasta:</b>{3}</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{35} </b></td><td style=""border: solid 1px; border-color: #000000;"" colspan=""5"">{4}</td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr bgcolor=""#F2F2F2""><td colspan=""2"" rowspan=""1"" style=""text-align: center; border: solid 1px; border-color: #000000;""><b>ASISTENTES </b></td><td colspan=""4"" rowspan=""1"" style=""text-align: center; border: solid 1px; border-color: #000000;""><b>FIRMAS </b></td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""1"">{14}</td><td style=""border: solid 1px; border-color: #000000;"" colspan=""4"" rowspan=""1"">&nbsp;</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""1"">{15}</td><td style=""border: solid 1px; border-color: #000000;"" colspan=""4"" rowspan=""1"">&nbsp;</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""1"">{16}</td><td style=""border: solid 1px; border-color: #000000;"" colspan=""4"" rowspan=""1"">&nbsp;</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""1"">{17}</td><td style=""border: solid 1px; border-color: #000000;"" colspan=""4"" rowspan=""1"">&nbsp;</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""1"">{18}</td><td style=""border: solid 1px; border-color: #000000;"" colspan=""4"" rowspan=""1"">&nbsp;</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""1"">{19}</td><td style=""border: solid 1px; border-color: #000000;"" colspan=""4"" rowspan=""1"">&nbsp;</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""1"">{20}</td><td style=""border: solid 1px; border-color: #000000;"" colspan=""4"" rowspan=""1"">&nbsp;</td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""6"" bgcolor=""#F2F2F2""><b>ORDEN DEL DÍA </b></td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""6"">{21}</td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr><td colspan=""2"" rowspan=""1"" style=""text-align: center; border: solid 1px; border-color: #000000;""bgcolor=""#F2F2F2""><b>COMPROMISOS </b></td><td style=""border: solid 1px; border-color: #000000; text-align: center;"" bgcolor=""#F2F2F2""colspan=""3""><b>RESPONSABLE </b></td><td style=""border: solid 1px; border-color: #000000; text-align: center;"" bgcolor=""#F2F2F2""><b>FECHA DE SEGUIMIENTO </b></td></tr>{40}</table></html>"

#End Region

#Region "Propiedades acta de seguimiento"

    Public Property ProceedVersion() As String
        Get
            Return Me._ProceedVersion
        End Get
        Set(ByVal value As String)
            Me._ProceedVersion = value
        End Set
    End Property
    Public Property ProceedCode() As String
        Get
            Return Me._ProceedCode
        End Get
        Set(ByVal value As String)
            Me._ProceedCode = value
        End Set
    End Property
    Public Property ProceedApprovalDate() As String
        Get
            Return Me._ProceedApprovalDate
        End Get
        Set(ByVal value As String)
            Me._ProceedApprovalDate = value
        End Set
    End Property
    Public Property ProceedNumberOfPages() As String
        Get
            Return Me._ProceedNumberOfPages
        End Get
        Set(ByVal value As String)
            Me._ProceedNumberOfPages = value
        End Set
    End Property
    Public Property RecordNumber() As String
        Get
            Return Me._RecordNumber
        End Get
        Set(ByVal value As String)
            Me._RecordNumber = value
        End Set
    End Property
    Public Property SignedRecord() As String
        Get
            Return Me._SignedRecord
        End Get
        Set(ByVal value As String)
            Me._SignedRecord = value
        End Set
    End Property
    Public Property ComitteeDate() As String
        Get
            Return Me._ComitteeDate
        End Get
        Set(ByVal value As String)
            Me._ComitteeDate = value
        End Set
    End Property
    Public Property ArchivedRecord() As String
        Get
            Return Me._ArchivedRecord
        End Get
        Set(ByVal value As String)
            Me._ArchivedRecord = value
        End Set
    End Property
    Public Property PartnerOperatorName() As String
        Get
            Return Me._PartnerOperatorName
        End Get
        Set(ByVal value As String)
            Me._PartnerOperatorName = value
        End Set
    End Property
    Public Property ContractNumber() As String
        Get
            Return Me._ContractNumber
        End Get
        Set(ByVal value As String)
            Me._ContractNumber = value
        End Set
    End Property

    Public Property ContractStartDate() As String
        Get
            Return Me._ContractStartDate
        End Get
        Set(ByVal value As String)
            Me._ContractStartDate = value
        End Set
    End Property

    Public Property ContractFinishDate() As String
        Get
            Return Me._ContractFinishDate
        End Get
        Set(ByVal value As String)
            Me._ContractFinishDate = value
        End Set
    End Property

    Public Property ContractObject() As String
        Get
            Return Me._ContractObject
        End Get
        Set(ByVal value As String)
            Me._ContractObject = value
        End Set
    End Property

    Public Property ContractValue() As String
        Get
            Return Me._ContractValue
        End Get
        Set(ByVal value As String)
            Me._ContractValue = value
        End Set
    End Property
    Public Property ContractValueLetters() As String
        Get
            Return Me._ContractValueLetters
        End Get
        Set(ByVal value As String)
            Me._ContractValueLetters = value
        End Set
    End Property
    Public Property NumberOfAdditions() As String
        Get
            Return Me._NumberOfAdditions
        End Get
        Set(ByVal value As String)
            Me._NumberOfAdditions = value
        End Set
    End Property
    Public Property AdditionValueLetters() As String
        Get
            Return Me._AdditionValueLetters
        End Get
        Set(ByVal value As String)
            Me._AdditionValueLetters = value
        End Set
    End Property
    Public Property AdditionDate() As String
        Get
            Return Me._AdditionDate
        End Get
        Set(ByVal value As String)
            Me._AdditionDate = value
        End Set
    End Property
    Public Property AdditionValue() As String
        Get
            Return Me._AdditionValue
        End Get
        Set(ByVal value As String)
            Me._AdditionValue = value
        End Set
    End Property
    Public Property AdditionInLetters() As String
        Get
            Return Me._AdditionInLetters
        End Get
        Set(ByVal value As String)
            Me._AdditionInLetters = value
        End Set
    End Property
    Public Property Assistant1() As String
        Get
            Return Me._Assistant1
        End Get
        Set(ByVal value As String)
            Me._Assistant1 = value
        End Set
    End Property
    Public Property Assistant2() As String
        Get
            Return Me._Assistant2
        End Get
        Set(ByVal value As String)
            Me._Assistant2 = value
        End Set
    End Property
    Public Property Assistant3() As String
        Get
            Return Me._Assistant3
        End Get
        Set(ByVal value As String)
            Me._Assistant3 = value
        End Set
    End Property
    Public Property Assistant4() As String
        Get
            Return Me._Assistant4
        End Get
        Set(ByVal value As String)
            Me._Assistant4 = value
        End Set
    End Property
    Public Property Assistant5() As String
        Get
            Return Me._Assistant5
        End Get
        Set(ByVal value As String)
            Me._Assistant5 = value
        End Set
    End Property
    Public Property Assistant6() As String
        Get
            Return Me._Assistant6
        End Get
        Set(ByVal value As String)
            Me._Assistant6 = value
        End Set
    End Property
    Public Property Assistant7() As String
        Get
            Return Me._Assistant7
        End Get
        Set(ByVal value As String)
            Me._Assistant7 = value
        End Set
    End Property
    Public Property DayOrder1() As String
        Get
            Return Me._DayOrder1
        End Get
        Set(ByVal value As String)
            Me._DayOrder1 = value
        End Set
    End Property
    Public Property DayOrder2() As String
        Get
            Return Me._DayOrder2
        End Get
        Set(ByVal value As String)
            Me._DayOrder2 = value
        End Set
    End Property
    Public Property DayOrder3() As String
        Get
            Return Me._DayOrder3
        End Get
        Set(ByVal value As String)
            Me._DayOrder3 = value
        End Set
    End Property
    Public Property Liability1() As String
        Get
            Return Me._Liability1
        End Get
        Set(ByVal value As String)
            Me._Liability1 = value
        End Set
    End Property
    Public Property Liability2() As String
        Get
            Return Me._Liability2
        End Get
        Set(ByVal value As String)
            Me._Liability2 = value
        End Set
    End Property
    Public Property Liability3() As String
        Get
            Return Me._Liability3
        End Get
        Set(ByVal value As String)
            Me._Liability3 = value
        End Set
    End Property
    Public Property Liable1() As String
        Get
            Return Me._Liable1
        End Get
        Set(ByVal value As String)
            Me._Liable1 = value
        End Set
    End Property
    Public Property Liable2() As String
        Get
            Return Me._Liable2
        End Get
        Set(ByVal value As String)
            Me._Liable2 = value
        End Set
    End Property
    Public Property Liable3() As String
        Get
            Return Me._Liable3
        End Get
        Set(ByVal value As String)
            Me._Liable3 = value
        End Set
    End Property
    Public Property TracingDate1() As String
        Get
            Return Me._TracingDate1
        End Get
        Set(ByVal value As String)
            Me._TracingDate1 = value
        End Set
    End Property
    Public Property TracingDate2() As String
        Get
            Return Me._TracingDate2
        End Get
        Set(ByVal value As String)
            Me._TracingDate2 = value
        End Set
    End Property
    Public Property TracingDate3() As String
        Get
            Return Me._TracingDate3
        End Get
        Set(ByVal value As String)
            Me._TracingDate3 = value
        End Set
    End Property
    Public Property Tracing_Html() As String
        Get
            Return Me._Tracing_Html
        End Get
        Set(ByVal value As String)
            Me._Tracing_Html = value
        End Set
    End Property
    Public Property Aditions() As String
        Get
            Return Me._Aditions
        End Get
        Set(ByVal value As String)
            Me._Aditions = value
        End Set
    End Property
    Public Property ContractFinalValue() As String
        Get
            Return Me._ContractFinalValue
        End Get
        Set(ByVal value As String)
            Me._ContractFinalValue = value
        End Set
    End Property
    Public Property lblDatesTrace() As String
        Get
            Return Me._lblDatesTrace
        End Get
        Set(ByVal value As String)
            Me._lblDatesTrace = value
        End Set
    End Property
    Public Property lblObject() As String
        Get
            Return Me._lblObject
        End Get
        Set(ByVal value As String)
            Me._lblObject = value
        End Set
    End Property
    Public Property lblValue() As String
        Get
            Return Me._lblValue
        End Get
        Set(ByVal value As String)
            Me._lblValue = value
        End Set
    End Property
    Public Property lblAdditions() As String
        Get
            Return Me._lblAdditions
        End Get
        Set(ByVal value As String)
            Me._lblAdditions = value
        End Set
    End Property
    Public Property Label1() As String
        Get
            Return Me._Label1
        End Get
        Set(ByVal value As String)
            Me._Label1 = value
        End Set
    End Property
    Public Property lblTypeThird() As String
        Get
            Return Me._lblTypeThird
        End Get
        Set(ByVal value As String)
            Me._lblTypeThird = value
        End Set
    End Property
    Public Property lblProceedNumber() As String
        Get
            Return Me._lblProceedNumber
        End Get
        Set(ByVal value As String)
            Me._lblProceedNumber = value
        End Set
    End Property
    Public Property COMPROMISELIST() As List(Of CompromiseEntity)
        Get
            Return Me._COMPROMISELIST
        End Get
        Set(ByVal value As List(Of CompromiseEntity))
            Me._COMPROMISELIST = value
        End Set
    End Property
    Public Property TABLECOMPROMISE() As String
        Get
            Return Me._TABLECOMPROMISE
        End Get
        Set(ByVal value As String)
            Me._TABLECOMPROMISE = value
        End Set
    End Property

 

#End Region

#Region "Funciones"

    Function StartExportTrace() As String

        Try
            Dim FileName As String = String.Format("/Proceedings/Acta_de_Seguimiento_{1}_{2}.xls", _directorioActas, _Idproject, Convert.ToDateTime(DateTime.Now).ToString("yyy_MM_dd_hh_mm_ss"))
            Dim FullPath As String = String.Format("{0}{1}", _directorioActas, FileName)

            'agregar el resto de campos
            _Tracing_Html = String.Format(_Tracing_Html, _PartnerOperatorName, _ContractNumber, _ContractStartDate, _ContractFinishDate, _ContractObject, _ContractValue, _ContractValueLetters, _Aditions, _NumberOfAdditions, _AdditionValueLetters, _AdditionDate, _AdditionValue, _ContractFinalValue, _AdditionInLetters, _Assistant1, _Assistant2, _Assistant3, _Assistant4, _Assistant5, _Assistant6, _Assistant7, _DayOrder1, _Liability1, _Liable1, _TracingDate1, _Liability2, _Liable2, _TracingDate2, _Liability3, _Liable3, _TracingDate3, _ProceedCode, _ProceedApprovalDate, _lblTypeThird, _lblDatesTrace, _lblObject, _lblValue, _lblAdditions, _Label1, _lblProceedNumber, _TABLECOMPROMISE)
            WriteFile(FullPath, _Tracing_Html)

            Return FileName

        Catch ex As Exception
            Return ""
        End Try

    End Function

#End Region

End Class
