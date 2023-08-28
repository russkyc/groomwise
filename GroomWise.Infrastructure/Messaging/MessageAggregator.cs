// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using Injectio.Attributes;
using MvvmGen.Events;

namespace GroomWise.Infrastructure.Messaging;

[RegisterSingleton<IEventAggregator, MessageAggregator>]
public class MessageAggregator : EventAggregator { }
