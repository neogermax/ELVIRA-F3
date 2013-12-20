USE [FSC_eProject]
ALTER TABLE ProjectApprovalRecord add aportFSC numeric(18,0) NULL
ALTER TABLE ProjectApprovalRecord add aportOtros numeric(18,0) NULL
ALTER TABLE ProjectApprovalRecord alter column ApprovedValue numeric(18,0) NULL
