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

#Region "Campos acta de cierre"

    Private _TypeThird As String
    Private _CloseTypeoF As String
    Private _ThirdName As String
    Private _ContractNumberClose As String
    Private _InitialDate As String
    Private _FinalDate As String
    Private _ContractObjectClose As String
    Private _CloseLetters As String
    Private _InitialValue As String
    Private _LettersClose As String
    Private _Adition As String
    Private _NumberAdition As String
    Private _InLetters As String
    Private _AdditionDateClose As String
    Private _AditionValue As String
    Private _VigencyExtend As String
    Private _ContractFinalValueClose As String
    Private _ContractFinalValueInLetters As String
    Private _Fullfillment As String
    Private _Observations As String
    Private _Weakness As String
    Private _Oportunities As String
    Private _Strongest As String
    Private _Learnings As String
    Private _FinishDate As String
    Private _PartnerName1 As String
    Private _FSCName1 As String
    Private _PartnerName2 As String
    Private _FSCName2 As String
    Private _PartnerName3 As String
    Private _FSCName3 As String
    Private _Close_Html As String = "<html><meta http-equiv=""""Content-Type"""" content=""""text/html; charset=UTF-8"""" /><table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 80%;"" align=""center""><tbody><tr><td colspan=""2"" rowspan=""1"" style=""border: solid 1px; border-color: #000000;""><img id=""ctl00_headerLeft"" src=""../App_Themes/GattacaAdmin/Images/Template/header_leftBPO.jpg""style=""border-width: 0px;""></td><td colspan=""4"" style=""text-align: center; border: 1px solid #000000; font-size: large;""bgcolor=""#F2F2F2""><b>ACTA DE CIERRE</b></td></tr><tr><td colspan=""6""><p></p></td></tr><tr><td colspan=""2"" rowspan=""1"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>Nombre del {0}:</b></td><td colspan=""2"" style=""border: solid 1px; border-color: #000000; width: 25%;"" bgcolor=""#F2F2F2""><b>Número del {1}:</b></td><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{30}</b></td></tr><tr><td colspan=""2"" rowspan=""2"" style=""border: solid 1px; border-color: #000000;"">{2}</td><td colspan=""2"" rowspan=""2"" style=""border: solid 1px; border-color: #000000; text-align: center;"">{3}</td><td colspan=""2"" style=""border: solid 1px; border-color: #000000;""><b>Desde: </b><asp:TextBox ID=""txtInitialDate"" runat=""server"">{4}</td></tr><tr><td colspan=""2"" style=""border: solid 1px; border-color: #000000;""><b>Hasta: </b>{5}</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{31}</b></td><td colspan=""5"" rowspan=""1"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2"">{6}</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{38}: </b></td><td style=""border: solid 1px; border-color: #000000; text-align: left;"">{7}</td><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>En letras: </b></td><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"">{8}</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2"">   <b>{32}</b></td><td style=""border: solid 1px; border-color: #000000;"">{9}</td><td style=""border: solid 1px; border-color: #000000; width: 15%;"" bgcolor=""#F2F2F2""><b>Número de adiciones: </b></td><td style=""border: solid 1px; border-color: #000000; width: 15%; text-align: left;"">{10}</td><td style=""border: 1px solid #000000; width: 10%;"" bgcolor=""#F2F2F2""><b>En letras: </b></td><td style=""border: solid 1px; border-color: #000000; width: 15%;"">{11}</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>Fecha de la adición:</b></td><td style=""border: solid 1px; border-color: #000000; text-align: left;"">{12}</td><td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>Valor $:</b></td><td style=""border: solid 1px; border-color: #000000; text-align: left;"">{13}</td><td style=""border: 1px solid #000000; width: 85px; "" bgcolor=""#F2F2F2""><b>Ampliación de la vigencia en: </b></td><td style=""border: solid 1px; border-color: #000000;"">{14}</td></tr><tr><td style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{39}: </b></td><td style=""border: solid 1px; border-color: #000000;  text-align: left;"">{15}</td><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>En letras:</b></td><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"">{16}</td></tr><tr><td colspan=""6""><p></p></td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>Cumplimiento de responsabilidades contractuales:</b></td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"">{17}</td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>Observaciones:</b><p></p>{18}</td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{33}</b></td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"">{19}</td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{34}</b></td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"">{20}</td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{35}</b></td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"">{21}</td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>{36}</b></td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000;"">{22}</td></tr><tr><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>Fecha de cierre:</b></td><td colspan=""4"" style=""border: solid 1px; border-color: #000000; text-align: left;"">{23}</td></tr><tr><td colspan=""6""><p></p></td></tr><tr><td colspan=""6"" style=""border: solid 1px; border-color: #000000; text-align: center;""bgcolor=""#F2F2F2""><b>Asistentes</b></td></tr><tr><td colspan=""3"" style=""border: solid 1px; border-color: #000000; text-align: center; width: 50%;""bgcolor=""#F2F2F2""><b>{37}</b></td><td colspan=""3"" style=""border: solid 1px; border-color: #000000; text-align: center; width: 50%;""bgcolor=""#F2F2F2""><b>Fundación Saldarriaga Concha</b></td></tr><tr><td colspan=""2"" style=""border: solid 1px; border-color: #000000; text-align: center;""bgcolor=""#F2F2F2""><b>Nombre</b></td><td style=""border: solid 1px; border-color: #000000; text-align: center;"" bgcolor=""#F2F2F2""><b>Firma</b></td><td colspan=""2"" style=""border: solid 1px; border-color: #000000; text-align: center;""bgcolor=""#F2F2F2""><b style=""width: 25%"">Nombre</b></td><td style=""border: solid 1px; border-color: #000000; text-align: center; width: 25%;"" bgcolor=""#F2F2F2""><b>Firma</b></td></tr><tr><td colspan=""2"" style=""border: solid 1px; border-color: #000000; width: 25%;"">{24}</td><td style=""border: solid 1px; border-color: #000000; width: 25%;""></td><td colspan=""2"" style=""border: 1px solid #000000; "">{25}</td><td style=""border: solid 1px; border-color: #000000; width: 25%;""></td></tr><tr><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"">{26}</td><td style=""border: solid 1px; border-color: #000000;"" width=""33%""></td><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"">{27}</td><td style=""border: solid 1px; border-color: #000000;""></td></tr><tr><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"">{28}</td><td style=""border: solid 1px; border-color: #000000;""></td><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"">{29}</td><td style=""border: solid 1px; border-color: #000000;""></td></tr></tbody></table></html>"

    'campos adicionales (Labels dinamicos)
    Private _lblDates As String
    Private _lblObjectContract As String
    Private _lblContractAditions As String
    Private _lblWeakness As String
    Private _lblOportunity As String
    Private _lblStrong As String
    Private _lblLearning As String
    Private _lblOperator As String
    Private _lblContractInitialValue As String
    Private _lblFinalValue As String

#End Region

#Region "Propiedades acta de cierre"

    Public Property TypeThird() As String
        Get
            Return Me._TypeThird
        End Get
        Set(ByVal value As String)
            Me._TypeThird = value
        End Set
    End Property

    Public Property CloseTypeoF() As String
        Get
            Return Me._CloseTypeoF
        End Get
        Set(ByVal value As String)
            Me._CloseTypeoF = value
        End Set
    End Property

    Public Property ThirdName() As String
        Get
            Return Me._ThirdName
        End Get
        Set(ByVal value As String)
            Me._ThirdName = value
        End Set
    End Property

    Public Property ContractNumberClose() As String
        Get
            Return Me._ContractNumberClose
        End Get
        Set(ByVal value As String)
            Me._ContractNumberClose = value
        End Set
    End Property

    Public Property InitialDate() As String
        Get
            Return Me._InitialDate
        End Get
        Set(ByVal value As String)
            Me._InitialDate = value
        End Set
    End Property

    Public Property FinalDate() As String
        Get
            Return Me._FinalDate
        End Get
        Set(ByVal value As String)
            Me._FinalDate = value
        End Set
    End Property

    Public Property ContractObjectClose() As String
        Get
            Return Me._ContractObjectClose
        End Get
        Set(ByVal value As String)
            Me._ContractObjectClose = value
        End Set
    End Property

    Public Property CloseLetters() As String
        Get
            Return Me._CloseLetters
        End Get
        Set(ByVal value As String)
            Me._CloseLetters = value
        End Set
    End Property

    Public Property InitialValue() As String
        Get
            Return Me._InitialValue
        End Get
        Set(ByVal value As String)
            Me._InitialValue = value
        End Set
    End Property

    Public Property LettersClose() As String
        Get
            Return Me._LettersClose
        End Get
        Set(ByVal value As String)
            Me._LettersClose = value
        End Set
    End Property

    Public Property Adition() As String
        Get
            Return Me._Adition
        End Get
        Set(ByVal value As String)
            Me._Adition = value
        End Set
    End Property

    Public Property NumberAdition() As String
        Get
            Return Me._NumberAdition
        End Get
        Set(ByVal value As String)
            Me._NumberAdition = value
        End Set
    End Property

    Public Property InLetters() As String
        Get
            Return Me._InLetters
        End Get
        Set(ByVal value As String)
            Me._InLetters = value
        End Set
    End Property

    Public Property AdditionDateClose() As String
        Get
            Return Me._AdditionDateClose
        End Get
        Set(ByVal value As String)
            Me._AdditionDateClose = value
        End Set
    End Property

    Public Property AditionValue() As String
        Get
            Return Me._AditionValue
        End Get
        Set(ByVal value As String)
            Me._AditionValue = value
        End Set
    End Property

    Public Property VigencyExtend() As String
        Get
            Return Me._VigencyExtend
        End Get
        Set(ByVal value As String)
            Me._VigencyExtend = value
        End Set
    End Property

    Public Property ContractFinalValueClose() As String
        Get
            Return Me._ContractFinalValueClose
        End Get
        Set(ByVal value As String)
            Me._ContractFinalValueClose = value
        End Set
    End Property

    Public Property ContractFinalValueInLetters() As String
        Get
            Return Me._ContractFinalValueInLetters
        End Get
        Set(ByVal value As String)
            Me._ContractFinalValueInLetters = value
        End Set
    End Property

    Public Property Fullfillment() As String
        Get
            Return Me._Fullfillment
        End Get
        Set(ByVal value As String)
            Me._Fullfillment = value
        End Set
    End Property

    Public Property Observations() As String
        Get
            Return Me._Observations
        End Get
        Set(ByVal value As String)
            Me._Observations = value
        End Set
    End Property

    Public Property Weakness() As String
        Get
            Return Me._Weakness
        End Get
        Set(ByVal value As String)
            Me._Weakness = value
        End Set
    End Property

    Public Property Oportunities() As String
        Get
            Return Me._Oportunities
        End Get
        Set(ByVal value As String)
            Me._Oportunities = value
        End Set
    End Property

    Public Property Strongest() As String
        Get
            Return Me._Strongest
        End Get
        Set(ByVal value As String)
            Me._Strongest = value
        End Set
    End Property

    Public Property Learnings() As String
        Get
            Return Me._Learnings
        End Get
        Set(ByVal value As String)
            Me._Learnings = value
        End Set
    End Property

    Public Property FinishDate() As String
        Get
            Return Me._FinishDate
        End Get
        Set(ByVal value As String)
            Me._FinishDate = value
        End Set
    End Property

    Public Property PartnerName1() As String
        Get
            Return Me._PartnerName1
        End Get
        Set(ByVal value As String)
            Me._PartnerName1 = value
        End Set
    End Property

    Public Property FSCName1() As String
        Get
            Return Me._FSCName1
        End Get
        Set(ByVal value As String)
            Me._FSCName1 = value
        End Set
    End Property

    Public Property PartnerName2() As String
        Get
            Return Me._PartnerName2
        End Get
        Set(ByVal value As String)
            Me._PartnerName2 = value
        End Set
    End Property

    Public Property FSCName2() As String
        Get
            Return Me._FSCName2
        End Get
        Set(ByVal value As String)
            Me._FSCName2 = value
        End Set
    End Property

    Public Property PartnerName3() As String
        Get
            Return Me._PartnerName3
        End Get
        Set(ByVal value As String)
            Me._PartnerName3 = value
        End Set
    End Property

    Public Property FSCName3() As String
        Get
            Return Me._FSCName3
        End Get
        Set(ByVal value As String)
            Me._FSCName3 = value
        End Set
    End Property

    Public Property lblDates() As String
        Get
            Return Me._lblDates
        End Get
        Set(ByVal value As String)
            Me._lblDates = value
        End Set
    End Property


    Public Property lblObjectContract() As String
        Get
            Return Me._lblObjectContract
        End Get
        Set(ByVal value As String)
            Me._lblObjectContract = value
        End Set
    End Property


    Public Property lblContractAditions() As String
        Get
            Return Me._lblContractAditions
        End Get
        Set(ByVal value As String)
            Me._lblContractAditions = value
        End Set
    End Property


    Public Property lblWeakness() As String
        Get
            Return Me._lblWeakness
        End Get
        Set(ByVal value As String)
            Me._lblWeakness = value
        End Set
    End Property


    Public Property lblOportunity() As String
        Get
            Return Me._lblOportunity
        End Get
        Set(ByVal value As String)
            Me._lblOportunity = value
        End Set
    End Property


    Public Property lblStrong() As String
        Get
            Return Me._lblStrong
        End Get
        Set(ByVal value As String)
            Me._lblStrong = value
        End Set
    End Property


    Public Property lblLearning() As String
        Get
            Return Me._lblLearning
        End Get
        Set(ByVal value As String)
            Me._lblLearning = value
        End Set
    End Property


    Public Property lblOperator() As String
        Get
            Return Me._lblOperator
        End Get
        Set(ByVal value As String)
            Me._lblOperator = value
        End Set
    End Property


    Public Property lblContractInitialValue() As String
        Get
            Return Me._lblContractInitialValue
        End Get
        Set(ByVal value As String)
            Me._lblContractInitialValue = value
        End Set
    End Property


    Public Property lblFinalValue() As String
        Get
            Return Me._lblFinalValue
        End Get
        Set(ByVal value As String)
            Me._lblFinalValue = value
        End Set
    End Property

#End Region

#Region "Funciones"

    Function StartExportClose() As String

        Try
            Dim FileName As String = String.Format("/Proceedings/Acta_de_Cierre_{1}_{2}.xls", _directorioActas, _Idproject, Convert.ToDateTime(DateTime.Now).ToString("yyy_MM_dd_hh_mm_ss"))
            Dim FullPath As String = String.Format("{0}{1}", _directorioActas, FileName)

            'agregar el resto de campos
            _Close_Html = String.Format(_Close_Html, _TypeThird, _CloseTypeoF, _ThirdName, _ContractNumberClose, _InitialDate, _FinalDate, _ContractObjectClose, _InitialValue, _LettersClose, _Adition, _NumberAdition, _InLetters, _AdditionDateClose, _AditionValue, _VigencyExtend, _ContractFinalValueClose, _ContractFinalValueInLetters, _Fullfillment, _Observations, _Weakness, _Oportunities, _Strongest, _Learnings, _FinishDate, _PartnerName1, _FSCName1, _PartnerName2, _FSCName2, _PartnerName3, _FSCName3, _lblDates, _lblObjectContract, _lblContractAditions, _lblWeakness, _lblOportunity, _lblStrong, _lblLearning, _lblOperator, _lblContractInitialValue, _lblFinalValue)
            WriteFile(FullPath, _Close_Html)

            Return FileName

        Catch ex As Exception
            Return ""
        End Try

    End Function


    Function ProceedLog(ByVal Project_id As String, ByVal Tipo_Acta_id As String, ByVal User_Id As String) As String



        Return ""
    End Function


#End Region

End Class
