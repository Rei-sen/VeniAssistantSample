using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeniAssistantSample.Function;

public class PostShownVenueID : FunctionTool
{
    public PostShownVenueID()
    {
        FunctionData = new()
        {
            Name = "post_shown_venue_id",
            Description = "Post the venue ID that the user has seen",
            Parameters = new()
            {
                Required = new() { "VenueID" },
                Properties = new()
                {
                        {"VenueID", new ()
                        {
                                Type = "string",
                                Description = "The ID of the venue that the user has seen"
                            }
                        }
                    }
            }
        };
    }
}
