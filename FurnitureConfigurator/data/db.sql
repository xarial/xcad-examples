SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Doors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Width] [float] NOT NULL,
	[Height] [float] NOT NULL,
	[InStock] [bit] NOT NULL,
 CONSTRAINT [PK_Doors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Drawers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Width] [float] NOT NULL,
	[Height] [float] NULL,
	[Depth] [float] NOT NULL,
	[InStock] [bit] NOT NULL,
 CONSTRAINT [PK_Drawers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Frames](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Width] [float] NOT NULL,
	[Height] [float] NOT NULL,
	[Depth] [float] NOT NULL,
	[InStock] [bit] NOT NULL,
 CONSTRAINT [PK_Frames] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Handles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[InStock] [bit] NOT NULL,
 CONSTRAINT [PK_Handles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Panels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Width] [float] NOT NULL,
	[Height] [float] NOT NULL,
	[InStock] [bit] NOT NULL,
 CONSTRAINT [PK_Panels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Doors] ON 
GO
INSERT [Doors] ([Id], [Type], [Width], [Height], [InStock]) VALUES (1, 0, 419, 700, 1)
GO
INSERT [Doors] ([Id], [Type], [Width], [Height], [InStock]) VALUES (2, 0, 419, 600, 1)
GO
SET IDENTITY_INSERT [Doors] OFF
GO
SET IDENTITY_INSERT [Drawers] ON 
GO
INSERT [Drawers] ([Id], [Type], [Width], [Height], [Depth], [InStock]) VALUES (1, 0, 537, 199, 500, 1)
GO
INSERT [Drawers] ([Id], [Type], [Width], [Height], [Depth], [InStock]) VALUES (3, 0, 600, 250, 600, 1)
GO
SET IDENTITY_INSERT [Drawers] OFF
GO
SET IDENTITY_INSERT [Frames] ON 
GO
INSERT [Frames] ([Id], [Type], [Width], [Height], [Depth], [InStock]) VALUES (2, 0, 1800, 200, 500, 1)
GO
SET IDENTITY_INSERT [Frames] OFF
GO
SET IDENTITY_INSERT [Handles] ON 
GO
INSERT [Handles] ([Id], [Type], [InStock]) VALUES (1, 0, 1)
GO
INSERT [Handles] ([Id], [Type], [InStock]) VALUES (2, 1, 1)
GO
INSERT [Handles] ([Id], [Type], [InStock]) VALUES (3, 2, 1)
GO
SET IDENTITY_INSERT [Handles] OFF
GO
SET IDENTITY_INSERT [Panels] ON 
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (2, 2, 500, 682, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (4, 3, 1800, 500, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (6, 1, 500, 682, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (7, 0, 1764, 500, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (8, 4, 1764, 664, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (9, 5, 482, 664, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (11, 2, 500, 582, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (12, 3, 1800, 400, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (13, 1, 500, 582, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (15, 0, 1764, 400, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (16, 4, 1764, 564, 1)
GO
INSERT [Panels] ([Id], [Type], [Width], [Height], [InStock]) VALUES (17, 5, 482, 564, 1)
GO
SET IDENTITY_INSERT [Panels] OFF
GO
