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

#Region "campos acta de inicio"

    Private _dinamictp As String
    Private _dinamicnameSO As String
    Private _dinamicnumberCC As String
    Private _dinamicobjectCC As String
    Private _dinamicsuperviser As String
    Private _dinamicvalues As String
    Private _dinamicaportefect As String
    Private _dinamicaportefespe As String
    Private _dinamicdatesuscrip As String
    Private _dinamicduration As String
    Private _dinamicdatestart As String
    Private _dinamicdateend As String
    Private _dinamicoperating As String
    
    Private _OperatorName As String
    Private _NumberAgreementContract As String
    Private _ObjectOfTheAgreementContract As String
    Private _TitleName As String
    Private _AccompanimentProfessionalNameInitiative As String
    Private _SupervisorName As String
    Private _ContractAgreementValue As String
    Private _InLyrics As String
    Private _aportvaluesFSC As String
    Private _aportespecieFSC As String
    Private _aporvaluesother As String
    Private _aportespecieother As String
    Private _suscripdateofContractAgreement As String
    Private _DurationOfContract As String
    Private _StartDateOfContractAgreement As String
    Private _CompletionDateContractAgreement As String
    Private _InformationOnTheLegal As String
    Private _aseguradoraname As String
    Private _PolicyDetailsList As String
    Private _OperatingPartnerContractAgreement As String
    Private _Comments As String
    Private _dateend As String
    Private _cargo1 As String
    Private _cargo2 As String
    Private _cargo3 As String
    Private _cargo4 As String
    Private _cargo5 As String
    Private _cargo6 As String
    Private _name1 As String
    Private _name2 As String
    Private _name3 As String
    Private _name4 As String
    Private _name5 As String
    Private _name6 As String



    Private _directorioActas As String
    Private _Idproject As String
    'creamos el html seguido por la vista del aspx de acta de inicio
    Private _proceedingStar_html As String = "<html><meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /><table cellpadding=""1"" cellspacing=""1"" id=""Acta_Inicio"" style=""width: 80%;"" align=""center""><tr><td style=""border: 1px solid #000000; height: 80px;"" colspan=""2"" rowspan=""1""></td><td colspan=""4"" style=""text-align: center; border: 1px solid #000000; font-size: large; height: 80px;"""" bgcolor=""#F2F2F2""><b>ACTA DE INICIO </b></td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr><td align=""center"" colspan=""6"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>{46}</b></td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr><td colspan=""3"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>Nombre del {0}: </b></td><td colspan=""3"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>Número del {1}: </b></td></tr><tr><td colspan=""3"" style=""border: solid 1px #000000; height: 44px;"">{2}</td><td colspan=""3"" style=""border: solid 1px #000000; height: 44px;"">{3}</td></tr><tr><td colspan=""3"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>{4}</b></td><td colspan=""3"" style=""border: solid 1px #000000;"">{5}</td></tr><tr><td colspan=""3"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>Nombre </b>{6}<b> :</b></td><td colspan=""3"" style=""border: solid 1px #000000;"">{7}</td></tr><tr><td colspan=""3"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>{8}</b></td><td colspan=""3"" style=""border: solid 1px #000000;"">{9}</td></tr><tr><td colspan=""2"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>{10}</b></td><td style=""border: solid 1px #000000;"">{11}</td><td colspan=""2"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>En Letras: </b></td><td style=""border: solid 1px #000000;"">{12}</td></tr><tr><td colspan=""2"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>Aporte de la Fundación Saldarriaga Concha en Efectivo: </b></td><td style=""border: solid 1px #000000;"">{13}</td><td colspan=""2"" style=""border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;""><b>Aporte de la Fundación Saldarriaga Concha en Especie:</b></td><td style=""border: solid 1px #000000;"">{14}</td></tr><tr><td colspan=""2"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>{15}</b></td><td style=""border: solid 1px #000000;"">{16}</td><td colspan=""2"" style=""border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;""><b>{17}</b></td><td style=""border: solid 1px #000000;"">{18}</td></tr><tr><td colspan=""2"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>{19}</b></td><td style=""border: solid 1px #000000;"">{20}</td><td colspan=""2"" style=""border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;""><b>{21}</b></td><td style=""border: solid 1px #000000;"">{22}</td></tr><tr><td colspan=""2"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>{23}</b></td><td style=""border: solid 1px #000000;"">{24}</td><td colspan=""2"" style=""border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;""><b>{25}</b></td><td style=""border: solid 1px #000000;"">{26}</td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr><td align=""left"" colspan=""6"" style=""border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;""><b>Información sobre el soporte juridico</b></td></tr><tr><td align=""center"" colspan=""6"" style=""border: solid 1px #000000;"">{27}</td></tr><tr><td colspan=""3"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>Nombre de la aseguradora : </b></td><td colspan=""3"" style=""border: solid 1px #000000;"">{28}</td></tr><tr align=""center""><td style=""border: solid 1px #000000; background-color: #F2F2F2;"" rowspan=""2""><b>No. de la Poliza</b></td><td style=""border: solid 1px #000000; background-color: #F2F2F2;"" rowspan=""2"" ><b>Concepto</b></td><td style=""border: solid 1px #000000; background-color: #F2F2F2;"" colspan=""4""><b>Vigencia</b></td></tr><tr align=""center""><td style=""border: solid 1px #000000; background-color: #F2F2F2;"" colspan=""2""><b>Desde</b></td><td style=""border: solid 1px #000000; background-color: #F2F2F2;"" colspan=""2""><b>Hasta</b></td></tr>{29}<tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr><td align=""left"" colspan=""6"" style=""border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;"">{30}</td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr><td align=""center"" colspan=""6"" style=""border: solid 1px #000000;"">{31}</td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr><td align=""left"" colspan=""6"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>Observaciones</b></td></tr><tr><td align=""center"" colspan=""6"" style=""border: solid 1px #000000;"">{32}</td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr><tr><td colspan=""2"" style=""border: solid 1px; border-color: #000000;"" bgcolor=""#F2F2F2""><b>Para constancia de lo anterior, firman la presenta acta los que en ella intervinieron el, </b></td><td colspan=""4"" style=""border: solid 1px; border-color: #000000;"">{33}</td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: justify;""><p></p></td></tr><tr><td align=""left"" colspan=""6"" style=""border: solid 1px #000000; background-color: #F2F2F2;""><b>Firmas</b></td></tr><tr><td colspan=""6"" rowspan=""1"" style=""text-align: center;""><p></p></td></tr></table>	<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td  style=""width: 10%;""></td><td colspan=""2"" rowspan=""1""><strong>Firma</strong>___________________________</td><td colspan=""2"" rowspan=""1""><strong>Firma</strong>___________________________</td></tr><tr><td style=""width: 10%;""></td><td colspan=""2"" rowspan=""1""><strong>Nombre</strong>: &nbsp;{35}</td><td colspan=""2"" rowspan=""1""><strong>Nombre:</strong>&nbsp;{37}</td></tr><tr><td style=""width: 10%;""></td><td colspan=""2"" rowspan=""1""><strong>{34}</strong></td><td colspan=""2"" rowspan=""1""><strong>{36}</strong></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 10%;""></td><td colspan=""2"" rowspan=""1""><strong>Firma</strong>___________________________</td><td colspan=""2"" rowspan=""1""><strong>Firma</strong>___________________________</td></tr><tr><td style=""width: 10%;""></td><td colspan=""2"" rowspan=""1""><strong>Nombre:</strong>&nbsp;{39}</td><td colspan=""2"" rowspan=""1""><strong>Nombre:</strong> &nbsp;{41}</td></tr><tr><td style=""width: 10%;""></td><td colspan=""2"" rowspan=""1""><strong>{38}</strong></td><td colspan=""2"" rowspan=""1""><strong>{40}</strong></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 10%;""></td><td colspan=""2"" rowspan=""1""><strong>Firma</strong>___________________________</td><td colspan=""2"" rowspan=""1""><strong>Firma</strong>___________________________</td></tr><tr><td style=""width: 10%;""></td><td colspan=""2"" rowspan=""1""><strong>Nombre:</strong>&nbsp;{43}</td><td colspan=""2"" rowspan=""1""><strong>Nombre:</strong> &nbsp;{45}</td></tr><tr><td style=""width: 10%;""></td><td colspan=""2"" rowspan=""1""><strong>{42}</strong></td><td colspan=""2"" rowspan=""1""><strong>{44}</strong></td></tr></tbody></table></html>"

#End Region

#Region "propiedades acta de inicio"

    Public Property dinamictp() As String
        Get
            Return Me._dinamictp
        End Get
        Set(ByVal value As String)
            Me._dinamictp = value
        End Set
    End Property

    Public Property TitleName() As String
        Get
            Return Me._TitleName
        End Get
        Set(ByVal value As String)
            Me._TitleName = value
        End Set
    End Property
    Public Property aportvaluesFSC() As String
        Get
            Return Me._aportvaluesFSC
        End Get
        Set(ByVal value As String)
            Me._aportvaluesFSC = value
        End Set
    End Property
    Public Property aportespecieFSC() As String
        Get
            Return Me._aportespecieFSC
        End Get
        Set(ByVal value As String)
            Me._aportespecieFSC = value
        End Set
    End Property
    Public Property aporvaluesother() As String
        Get
            Return Me._aporvaluesother
        End Get
        Set(ByVal value As String)
            Me._aporvaluesother = value
        End Set
    End Property
    Public Property aportespecieother() As String
        Get
            Return Me._aportespecieother
        End Get
        Set(ByVal value As String)
            Me._aportespecieother = value
        End Set
    End Property
    Public Property suscripdateofContractAgreement() As String
        Get
            Return Me._suscripdateofContractAgreement
        End Get
        Set(ByVal value As String)
            Me._suscripdateofContractAgreement = value
        End Set
    End Property
    Public Property OperatorName() As String
        Get
            Return Me._OperatorName
        End Get
        Set(ByVal value As String)
            Me._OperatorName = value
        End Set
    End Property
    Public Property NumberAgreementContract() As String
        Get
            Return Me._NumberAgreementContract
        End Get
        Set(ByVal value As String)
            Me._NumberAgreementContract = value
        End Set
    End Property
    Public Property ObjectOfTheAgreementContract() As String
        Get
            Return Me._ObjectOfTheAgreementContract
        End Get
        Set(ByVal value As String)
            Me._ObjectOfTheAgreementContract = value
        End Set
    End Property
    Public Property AccompanimentProfessionalNameInitiative() As String
        Get
            Return Me._AccompanimentProfessionalNameInitiative
        End Get
        Set(ByVal value As String)
            Me._AccompanimentProfessionalNameInitiative = value
        End Set
    End Property
    Public Property SupervisorName() As String
        Get
            Return Me._SupervisorName
        End Get
        Set(ByVal value As String)
            Me._SupervisorName = value
        End Set
    End Property
    Public Property ContractAgreementValue() As String
        Get
            Return Me._ContractAgreementValue
        End Get
        Set(ByVal value As String)
            Me._ContractAgreementValue = value
        End Set
    End Property
    Public Property InLyrics() As String
        Get
            Return Me._InLyrics
        End Get
        Set(ByVal value As String)
            Me._InLyrics = value
        End Set
    End Property
    Public Property StartDateOfContractAgreement() As String
        Get
            Return Me._StartDateOfContractAgreement
        End Get
        Set(ByVal value As String)
            Me._StartDateOfContractAgreement = value
        End Set
    End Property
    Public Property CompletionDateContractAgreement() As String
        Get
            Return Me._CompletionDateContractAgreement
        End Get
        Set(ByVal value As String)
            Me._CompletionDateContractAgreement = value
        End Set
    End Property
    Public Property DurationOfContract() As String
        Get
            Return Me._DurationOfContract
        End Get
        Set(ByVal value As String)
            Me._DurationOfContract = value
        End Set
    End Property
    Public Property InformationOnTheLegal() As String
        Get
            Return Me._InformationOnTheLegal
        End Get
        Set(ByVal value As String)
            Me._InformationOnTheLegal = value
        End Set
    End Property
    Public Property dateend() As String
        Get
            Return Me._dateend
        End Get
        Set(ByVal value As String)
            Me._dateend = value
        End Set
    End Property
    Public Property OperatingPartnerContractAgreement() As String
        Get
            Return Me._OperatingPartnerContractAgreement
        End Get
        Set(ByVal value As String)
            Me._OperatingPartnerContractAgreement = value
        End Set
    End Property
    Public Property Comments() As String
        Get
            Return Me._Comments
        End Get
        Set(ByVal value As String)
            Me._Comments = value
        End Set
    End Property
    Public Property PolicyDetailsList() As String
        Get
            Return Me._PolicyDetailsList
        End Get
        Set(ByVal value As String)
            Me._PolicyDetailsList = value
        End Set
    End Property
    Public Property Idproject() As String
        Get
            Return Me._Idproject
        End Get
        Set(ByVal value As String)
            Me._Idproject = value
        End Set
    End Property
    Public Property DirectorioActas() As String
        Get
            Return Me._directorioActas
        End Get
        Set(ByVal value As String)
            Me._directorioActas = value
        End Set
    End Property
    Public Property aseguradoraname() As String
        Get
            Return Me._aseguradoraname
        End Get
        Set(ByVal value As String)
            Me._aseguradoraname = value
        End Set
    End Property
    Public Property cargo1() As String
        Get
            Return Me._cargo1
        End Get
        Set(ByVal value As String)
            Me._cargo1 = value
        End Set
    End Property
    Public Property cargo2() As String
        Get
            Return Me._cargo2
        End Get
        Set(ByVal value As String)
            Me._cargo2 = value
        End Set
    End Property
    Public Property cargo3() As String
        Get
            Return Me._cargo3
        End Get
        Set(ByVal value As String)
            Me._cargo3 = value
        End Set
    End Property
    Public Property cargo4() As String
        Get
            Return Me._cargo4
        End Get
        Set(ByVal value As String)
            Me._cargo4 = value
        End Set
    End Property
    Public Property cargo5() As String
        Get
            Return Me._cargo5
        End Get
        Set(ByVal value As String)
            Me._cargo5 = value
        End Set
    End Property
    Public Property cargo6() As String
        Get
            Return Me._cargo6
        End Get
        Set(ByVal value As String)
            Me._cargo6 = value
        End Set
    End Property
    Public Property name1() As String
        Get
            Return Me._name1
        End Get
        Set(ByVal value As String)
            Me._name1 = value
        End Set
    End Property
    Public Property name2() As String
        Get
            Return Me._name2
        End Get
        Set(ByVal value As String)
            Me._name2 = value
        End Set
    End Property
    Public Property name3() As String
        Get
            Return Me._name3
        End Get
        Set(ByVal value As String)
            Me._name3 = value
        End Set
    End Property
    Public Property name4() As String
        Get
            Return Me._name4
        End Get
        Set(ByVal value As String)
            Me._name4 = value
        End Set
    End Property
    Public Property name5() As String
        Get
            Return Me._name5
        End Get
        Set(ByVal value As String)
            Me._name5 = value
        End Set
    End Property
    Public Property name6() As String
        Get
            Return Me._name6
        End Get
        Set(ByVal value As String)
            Me._name6 = value
        End Set
    End Property

    Public Property dinamicnameSO() As String
        Get
            Return Me._dinamicnameSO
        End Get
        Set(ByVal value As String)
            Me._dinamicnameSO = value
        End Set
    End Property
    Public Property dinamicnumberCC() As String
        Get
            Return Me._dinamicnumberCC
        End Get
        Set(ByVal value As String)
            Me._dinamicnumberCC = value
        End Set
    End Property
    Public Property dinamicobjectCC() As String
        Get
            Return Me._dinamicobjectCC
        End Get
        Set(ByVal value As String)
            Me._dinamicobjectCC = value
        End Set
    End Property
    Public Property dinamicsuperviser() As String
        Get
            Return Me._dinamicsuperviser
        End Get
        Set(ByVal value As String)
            Me._dinamicsuperviser = value
        End Set
    End Property
    Public Property dinamicvalues() As String
        Get
            Return Me._dinamicvalues
        End Get
        Set(ByVal value As String)
            Me._dinamicvalues = value
        End Set
    End Property
    Public Property dinamicaportefect() As String
        Get
            Return Me._dinamicaportefect
        End Get
        Set(ByVal value As String)
            Me._dinamicaportefect = value
        End Set
    End Property
    Public Property dinamicaportefespe() As String
        Get
            Return Me._dinamicaportefespe
        End Get
        Set(ByVal value As String)
            Me._dinamicaportefespe = value
        End Set
    End Property
    Public Property dinamicdatesuscrip() As String
        Get
            Return Me._dinamicdatesuscrip
        End Get
        Set(ByVal value As String)
            Me._dinamicdatesuscrip = value
        End Set
    End Property
    Public Property dinamicduration() As String
        Get
            Return Me._dinamicduration
        End Get
        Set(ByVal value As String)
            Me._dinamicduration = value
        End Set
    End Property
    Public Property dinamicdatestart() As String
        Get
            Return Me._dinamicdatestart
        End Get
        Set(ByVal value As String)
            Me._dinamicdatestart = value
        End Set
    End Property
    Public Property dinamicdateend() As String
        Get
            Return Me._dinamicdateend
        End Get
        Set(ByVal value As String)
            Me._dinamicdateend = value
        End Set
    End Property
    Public Property dinamicoperating() As String
        Get
            Return Me._dinamicoperating
        End Get
        Set(ByVal value As String)
            Me._dinamicoperating = value
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
            Dim nameFile As String = String.Format("/Proceedings/Acta_de_Inicio_{1}_{2}.xls", _directorioActas, _Idproject, Convert.ToDateTime(DateTime.Now).ToString("yyyy_MM_dd_hh_mm_ss"))
            Dim fullPath As String = String.Format("{0}{1}", _directorioActas, nameFile)

            _proceedingStar_html = String.Format(_proceedingStar_html, _dinamicnameSO, _dinamicnumberCC, _OperatorName, _NumberAgreementContract, _dinamicobjectCC, _ObjectOfTheAgreementContract, _TitleName, _AccompanimentProfessionalNameInitiative, _dinamicsuperviser, _SupervisorName, _dinamicvalues, _ContractAgreementValue, _InLyrics, _aportvaluesFSC, _aportespecieFSC, _dinamicaportefect, _aporvaluesother, _dinamicaportefespe, _aportespecieother, _dinamicdatesuscrip, _suscripdateofContractAgreement, _dinamicduration, _DurationOfContract, _dinamicdatestart, _StartDateOfContractAgreement, _dinamicdateend, _CompletionDateContractAgreement, _InformationOnTheLegal, _aseguradoraname, _PolicyDetailsList, _dinamicoperating, _OperatingPartnerContractAgreement, _Comments, _dateend, _cargo1, _name1, _cargo2, _name2, _cargo3, _name3, _cargo4, _name4, _cargo5, _name5, _cargo6, _name6, _dinamictp)
            WriteFile(fullPath, _proceedingStar_html)

            Return nameFile

        Catch ex As Exception
            Return ""

        End Try

    End Function

#End Region

End Class
