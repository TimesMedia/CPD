CREATE TABLE ResultDetail ( 
	ResultDetailId int identity(1,1)  NOT NULL,
	ResultId int NOT NULL,
	Datum datetime2(7) NOT NULL,
	AnswerId int NULL
)
;

CREATE TABLE Survey ( 
	SurveyId int identity(1,1)  NOT NULL,
	Naam nvarchar(80) NULL,
	ExpirationDate date NOT NULL
)
;

CREATE TABLE Result ( 
	ResultId int identity(1,1)  NOT NULL,
	CustomerId int NOT NULL,
	ModuleId int NOT NULL,
	Attempt smallint NOT NULL,
	Datum datetime2(7) NULL,
	Score decimal(10,2) NULL
)
;

CREATE TABLE Answer ( 
	AnswerId int identity(1,1)  NOT NULL,
	QuestionId int NOT NULL,
	Answer nvarchar(200) NOT NULL,
	Correct bit NULL
)
;

CREATE TABLE Question ( 
	QuestionId int identity(1,1)  NOT NULL,
	Question nvarchar(200) NOT NULL,
	ArticleId int NOT NULL
)
;

CREATE TABLE Article ( 
	ArticleId int identity(1,1)  NOT NULL,
	Naam nvarchar(80) NOT NULL,
	ModuleId int NOT NULL
)
;

CREATE TABLE Module ( 
	ModuleId int identity(1,1)  NOT NULL,
	Naam nvarchar(50) NOT NULL,
	SurveyId int NOT NULL,
	Points int NOT NULL,
	Passrate decimal(10,2) DEFAULT 0.8 NOT NULL
)
;

CREATE TABLE Accreditation ( 
	AccreditationId int identity(1,1)  NOT NULL,
	IssueId int NOT NULL,
	SurveyId int NOT NULL,
	AccreditationNumber nvarchar(50) NOT NULL,
	CMEPublication nvarchar(80) NOT NULL,
	CMEIssue nvarchar(50) NOT NULL
)
;


ALTER TABLE ResultDetail ADD CONSTRAINT PK_ResultDetail 
	PRIMARY KEY CLUSTERED (ResultDetailId)
;

ALTER TABLE Survey ADD CONSTRAINT PK_Survey 
	PRIMARY KEY CLUSTERED (SurveyId)
;

ALTER TABLE Result ADD CONSTRAINT PK_Result 
	PRIMARY KEY CLUSTERED (ResultId)
;

ALTER TABLE Answer ADD CONSTRAINT PK_Answer 
	PRIMARY KEY CLUSTERED (AnswerId)
;

ALTER TABLE Question ADD CONSTRAINT PK_Question 
	PRIMARY KEY CLUSTERED (QuestionId)
;

ALTER TABLE Article ADD CONSTRAINT PK_Article 
	PRIMARY KEY CLUSTERED (ArticleId)
;

ALTER TABLE Module ADD CONSTRAINT PK_Section 
	PRIMARY KEY CLUSTERED (ModuleId)
;

ALTER TABLE Accreditation ADD CONSTRAINT PK_Accreditation 
	PRIMARY KEY CLUSTERED (AccreditationId)
;


ALTER TABLE ResultDetail
	ADD CONSTRAINT UQ_ResultDetail_ResultDetailId UNIQUE (ResultDetailId)
;

ALTER TABLE Survey
	ADD CONSTRAINT UQ_Survey_SurveyId UNIQUE (SurveyId)
;

ALTER TABLE Result
	ADD CONSTRAINT UQ_Result_CustomerId UNIQUE (CustomerId)
;

ALTER TABLE Result
	ADD CONSTRAINT UQ_Result_ModuleId UNIQUE (ModuleId)
;

ALTER TABLE Result
	ADD CONSTRAINT UQ_Result_ResultId UNIQUE (ResultId)
;

ALTER TABLE Answer
	ADD CONSTRAINT UQ_Answer_AnswerId UNIQUE (AnswerId)
;

ALTER TABLE Question
	ADD CONSTRAINT UQ_Question_QuestionId UNIQUE (QuestionId)
;

ALTER TABLE Article
	ADD CONSTRAINT UQ_Article_ArticleId UNIQUE (ArticleId)
;

ALTER TABLE Module
	ADD CONSTRAINT UQ_Module_ModuleId UNIQUE (ModuleId)
;

ALTER TABLE Accreditation
	ADD CONSTRAINT UQ_Accreditation_AccreditationId UNIQUE (AccreditationId)
;

ALTER TABLE Accreditation
	ADD CONSTRAINT UQ_Accreditation_AccreditationNumber UNIQUE (AccreditationNumber)
;


ALTER TABLE ResultDetail ADD CONSTRAINT FK_ResultDetail_Result 
	FOREIGN KEY (ResultId) REFERENCES Result (ResultId)
;

ALTER TABLE Result ADD CONSTRAINT FK_Result_Module 
	FOREIGN KEY (ModuleId) REFERENCES Module (ModuleId)
;

ALTER TABLE Answer ADD CONSTRAINT FK_Answer_Question 
	FOREIGN KEY (QuestionId) REFERENCES Question (QuestionId)
;

ALTER TABLE Question ADD CONSTRAINT FK_Question_Article 
	FOREIGN KEY (ArticleId) REFERENCES Article (ArticleId)
;

ALTER TABLE Module ADD CONSTRAINT FK_Module_Survey 
	FOREIGN KEY (SurveyId) REFERENCES Survey (SurveyId)
;

ALTER TABLE Accreditation ADD CONSTRAINT FK_Accreditation_Survey 
	FOREIGN KEY (SurveyId) REFERENCES Survey (SurveyId)
	ON DELETE CASCADE ON UPDATE NO ACTION
;
