# Overview of the Solution

* All the specified business cases and requirements are implemented in the ForensicsCaseLibrary, including automatic case number generation, case approval/rejection, exhibit management, and cost calculation with discount rules (including the 100+ exhibits rule).
* Covered the key functions with unit tests ( including pricing which is probably not a great idea as prices can be subject to change in a real prod environment )
* Covered case state changes with tests as well
* Added an API project to be able to easily manually test and interact with the library. No authentification & the APIs are locally hosted. There are manual test cases in ForensicsCaseLibraryAPI/ForensicsCaseLibraryAPI.http
  * Running the API project launches Swagger, enabling convenient interaction with the APIs directly from the browser.

# Challenge

The task is to develop a library that handles incoming Forensics cases.

The library should provide functionality for the following tasks:
* Receive a Case from Police
* Receive exhibits (e.g., bullet shells, swabs, _etc._) for a Case
* Possibility to reject or approve a Case
* List all Cases
* Calculate the total cost of each case

A Case will contain the following fields:
1. Case number
2. State (e.g., Approved, Rejected, On-hold)
3. Customer ID
4. Responsible person
5. Case type

An Exhibit will contain the following fields:
1. Type of the Exhibit
2. Date when the exhibit was picked up at the crime scene

Analysis pricing for different types of exhibits (per piece):
1. Bullet shell: 15 EUR
2. Swab: 5 EUR
3. Luggage: 150 EUR

Pricing rules:
* Base price for a case is 50 EUR
* For cases with at least 10 exhibits of the same type, there is a 5% discount
* For cases with at least 50 exhibits of the same type, there is a 15% discount
* For cases with at least 100 exhibits of any kind, there is a 20% discount (nice-to-have feature)

Considerations for the design:
* Future extensions may include additional case and exhibit types
* The base price may vary based on the case type in future extensions
* Assume that your library may be integrated into a project with data storage capabilities

You can skip authentication and authorization.  
UI or database implementation is not required for this task.

## Technical Requirements
* Use C# as the programming language
* Use any unit testing framework
* You can choose any additional tools and libraries you prefer

## What Gets Evaluated
* Accordance with business requirements described above
* Code quality (we value design guided by tests)
* Checkout-and-run convenience

## Have fun!
