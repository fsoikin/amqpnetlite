﻿//  ------------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation
//  All rights reserved. 
//  
//  Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this 
//  file except in compliance with the License. You may obtain a copy of the License at 
//  http://www.apache.org/licenses/LICENSE-2.0  
//  
//  THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
//  EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
//  CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR 
//  NON-INFRINGEMENT. 
// 
//  See the Apache Version 2.0 License for specific language governing permissions and 
//  limitations under the License.
//  ------------------------------------------------------------------------------------

namespace Amqp
{
    using System;
    using Amqp.Framing;

    class Delivery : INode
    {
        Message message;

        public ByteBuffer Buffer;

        public uint Handle;

        public byte[] Tag;

        public SequenceNumber DeliveryId;

        public int BytesTransfered;

        public DeliveryState State;

        public OutcomeCallback OnOutcome;

        public object UserToken;

        public bool Settled;

        public INode Previous { get; set; }

        public INode Next { get; set; }

        public Message Message
        {
            get { return this.message; }
            set { this.message = value; value.Delivery = this; }
        }

        public void OnStateChange(DeliveryState state)
        {
            this.State = state;
            if (this.OnOutcome != null)
            {
                this.OnOutcome(this.Message, state as Outcome, this.UserToken);
            }
        }
    }
}