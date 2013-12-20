Imports Microsoft.VisualBasic

Public Class SupplierQualificationEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idsupplierevaluation As Integer
    Private _contractsubject As Integer
    Private _contractualobligations As Integer
    Private _definedgoals As Integer
    Private _agreeddeadlines As Integer
    Private _totalitydeliveredproducts As Integer
    Private _requestsmadebyfsc As Integer
    Private _deliveryproductsservices As Integer
    Private _reporting As Integer
    Private _productquality As Integer
    Private _reportsquality As Integer
    Private _accompanimentquality As Integer
    Private _attentioncomplaintsclaims As Integer
    Private _returnedproducts As Integer
    Private _productvalueadded As Integer
    Private _accompanimentvalueadded As Integer
    Private _reportsvalueadded As Integer
    Private _projectplaneacion As Integer
    Private _methodologyimplemented As Integer
    Private _developmentprojectorganization As Integer
    Private _jointactivities As Integer
    Private _projectcontrol As Integer
    Private _servicestaffcompetence As Integer
    Private _suppliercompetence As Integer
    Private _informationconfidentiality As Integer
    Private _compliancepercentage As Double
    Private _opportunitypercentage As Double
    Private _qualitypercentage As Double
    Private _addedvaluepercentage As Double
    Private _methodologypercentage As Double
    Private _servicestaffcompetencepercentage As Double
    Private _confidentialitypercentage As Double

#End Region

#Region "Propiedades"

    Public Property id() As Integer
        Get
            Return Me._id
        End Get
        Set(ByVal value As Integer)
            Me._id = value
        End Set
    End Property
    Public Property idsupplierevaluation() As Integer
        Get
            Return Me._idsupplierevaluation
        End Get
        Set(ByVal value As Integer)
            Me._idsupplierevaluation = value
        End Set
    End Property
    Public Property contractsubject() As Integer
        Get
            Return Me._contractsubject
        End Get
        Set(ByVal value As Integer)
            Me._contractsubject = value
        End Set
    End Property
    Public Property contractualobligations() As Integer
        Get
            Return Me._contractualobligations
        End Get
        Set(ByVal value As Integer)
            Me._contractualobligations = value
        End Set
    End Property
    Public Property definedgoals() As Integer
        Get
            Return Me._definedgoals
        End Get
        Set(ByVal value As Integer)
            Me._definedgoals = value
        End Set
    End Property
    Public Property agreeddeadlines() As Integer
        Get
            Return Me._agreeddeadlines
        End Get
        Set(ByVal value As Integer)
            Me._agreeddeadlines = value
        End Set
    End Property
    Public Property totalitydeliveredproducts() As Integer
        Get
            Return Me._totalitydeliveredproducts
        End Get
        Set(ByVal value As Integer)
            Me._totalitydeliveredproducts = value
        End Set
    End Property
    Public Property requestsmadebyfsc() As Integer
        Get
            Return Me._requestsmadebyfsc
        End Get
        Set(ByVal value As Integer)
            Me._requestsmadebyfsc = value
        End Set
    End Property
    Public Property deliveryproductsservices() As Integer
        Get
            Return Me._deliveryproductsservices
        End Get
        Set(ByVal value As Integer)
            Me._deliveryproductsservices = value
        End Set
    End Property
    Public Property reporting() As Integer
        Get
            Return Me._reporting
        End Get
        Set(ByVal value As Integer)
            Me._reporting = value
        End Set
    End Property
    Public Property productquality() As Integer
        Get
            Return Me._productquality
        End Get
        Set(ByVal value As Integer)
            Me._productquality = value
        End Set
    End Property
    Public Property reportsquality() As Integer
        Get
            Return Me._reportsquality
        End Get
        Set(ByVal value As Integer)
            Me._reportsquality = value
        End Set
    End Property
    Public Property accompanimentquality() As Integer
        Get
            Return Me._accompanimentquality
        End Get
        Set(ByVal value As Integer)
            Me._accompanimentquality = value
        End Set
    End Property
    Public Property attentioncomplaintsclaims() As Integer
        Get
            Return Me._attentioncomplaintsclaims
        End Get
        Set(ByVal value As Integer)
            Me._attentioncomplaintsclaims = value
        End Set
    End Property
    Public Property returnedproducts() As Integer
        Get
            Return Me._returnedproducts
        End Get
        Set(ByVal value As Integer)
            Me._returnedproducts = value
        End Set
    End Property
    Public Property productvalueadded() As Integer
        Get
            Return Me._productvalueadded
        End Get
        Set(ByVal value As Integer)
            Me._productvalueadded = value
        End Set
    End Property
    Public Property accompanimentvalueadded() As Integer
        Get
            Return Me._accompanimentvalueadded
        End Get
        Set(ByVal value As Integer)
            Me._accompanimentvalueadded = value
        End Set
    End Property
    Public Property reportsvalueadded() As Integer
        Get
            Return Me._reportsvalueadded
        End Get
        Set(ByVal value As Integer)
            Me._reportsvalueadded = value
        End Set
    End Property
    Public Property projectplaneacion() As Integer
        Get
            Return Me._projectplaneacion
        End Get
        Set(ByVal value As Integer)
            Me._projectplaneacion = value
        End Set
    End Property
    Public Property methodologyimplemented() As Integer
        Get
            Return Me._methodologyimplemented
        End Get
        Set(ByVal value As Integer)
            Me._methodologyimplemented = value
        End Set
    End Property
    Public Property developmentprojectorganization() As Integer
        Get
            Return Me._developmentprojectorganization
        End Get
        Set(ByVal value As Integer)
            Me._developmentprojectorganization = value
        End Set
    End Property
    Public Property jointactivities() As Integer
        Get
            Return Me._jointactivities
        End Get
        Set(ByVal value As Integer)
            Me._jointactivities = value
        End Set
    End Property
    Public Property projectcontrol() As Integer
        Get
            Return Me._projectcontrol
        End Get
        Set(ByVal value As Integer)
            Me._projectcontrol = value
        End Set
    End Property
    Public Property servicestaffcompetence() As Integer
        Get
            Return Me._servicestaffcompetence
        End Get
        Set(ByVal value As Integer)
            Me._servicestaffcompetence = value
        End Set
    End Property
    Public Property suppliercompetence() As Integer
        Get
            Return Me._suppliercompetence
        End Get
        Set(ByVal value As Integer)
            Me._suppliercompetence = value
        End Set
    End Property
    Public Property informationconfidentiality() As Integer
        Get
            Return Me._informationconfidentiality
        End Get
        Set(ByVal value As Integer)
            Me._informationconfidentiality = value
        End Set
    End Property
    Public Property compliancepercentage() As Double
        Get
            Return Me._compliancepercentage
        End Get
        Set(ByVal value As Double)
            Me._compliancepercentage = value
        End Set
    End Property
    Public Property opportunitypercentage() As Double
        Get
            Return Me._opportunitypercentage
        End Get
        Set(ByVal value As Double)
            Me._opportunitypercentage = value
        End Set
    End Property
    Public Property qualitypercentage() As Double
        Get
            Return Me._qualitypercentage
        End Get
        Set(ByVal value As Double)
            Me._qualitypercentage = value
        End Set
    End Property
    Public Property addedvaluepercentage() As Double
        Get
            Return Me._addedvaluepercentage
        End Get
        Set(ByVal value As Double)
            Me._addedvaluepercentage = value
        End Set
    End Property
    Public Property methodologypercentage() As Double
        Get
            Return Me._methodologypercentage
        End Get
        Set(ByVal value As Double)
            Me._methodologypercentage = value
        End Set
    End Property
    Public Property servicestaffcompetencepercentage() As Double
        Get
            Return Me._servicestaffcompetencepercentage
        End Get
        Set(ByVal value As Double)
            Me._servicestaffcompetencepercentage = value
        End Set
    End Property
    Public Property confidentialitypercentage() As Double
        Get
            Return Me._confidentialitypercentage
        End Get
        Set(ByVal value As Double)
            Me._confidentialitypercentage = value
        End Set
    End Property

#End Region

End Class
