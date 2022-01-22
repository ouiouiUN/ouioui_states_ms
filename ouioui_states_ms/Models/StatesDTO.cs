using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace States_ms.Models
{
    public class StatesDTO
    {
        public long stateId { get; set; }
        public string userId { get; set; }
        public string mediaId { get; set; }
        public DateTime createdOn { get; set; }
        public string stateText { get; set; }

        public StatesDTO (State state){
            this.stateId = state.stateId;
            this.userId = state.userId;
            this.mediaId = state.mediaId;
            this.createdOn = state.createdOn;
            this.stateText = state.stateText;
        }
    }
}