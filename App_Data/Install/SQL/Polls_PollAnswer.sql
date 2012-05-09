CREATE TABLE [Polls_PollAnswer] (
		[AnswerID]               [int] IDENTITY(1, 1) NOT NULL,
		[AnswerText]             [nvarchar](200) NOT NULL,
		[AnswerOrder]            [int] NULL,
		[AnswerCount]            [int] NULL,
		[AnswerEnabled]          [bit] NULL,
		[AnswerPollID]           [int] NOT NULL,
		[AnswerGUID]             [uniqueidentifier] NOT NULL,
		[AnswerLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [Polls_PollAnswer]
	ADD
	CONSTRAINT [PK_Polls_PollAnswer]
	PRIMARY KEY
	NONCLUSTERED
	([AnswerID])
	
	
CREATE CLUSTERED INDEX [IX_Polls_PollAnswer_AnswerPollID_AnswerOrder_AnswerEnabled]
	ON [Polls_PollAnswer] ([AnswerOrder], [AnswerPollID], [AnswerEnabled])
	
	
ALTER TABLE [Polls_PollAnswer]
	WITH CHECK
	ADD CONSTRAINT [FK_Polls_PollAnswer_AnswerPollID_Polls_Poll]
	FOREIGN KEY ([AnswerPollID]) REFERENCES [Polls_Poll] ([PollID])
ALTER TABLE [Polls_PollAnswer]
	CHECK CONSTRAINT [FK_Polls_PollAnswer_AnswerPollID_Polls_Poll]
