
using System;
using System.Linq;
using System.Collections.Generic;

using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowRental.Specs.Steps
{
    [Binding]
    [Scope(Feature = "Rental")]
    public sealed class RentalStepDefinitions : RentalStepBase
    {
        public RentalStepDefinitions(RentalBook book): base(book) {}

    }
}
