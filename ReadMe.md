# New Validator For Asp.Net Core Models

This Package Create Some Validators To Easily Validate View Models And Dtos In Asp.Net Core Applications 

## How To Use

#### Check If Specific Property Is True Then This Property Is Require
usage :
```
public class TestViewModel {

    pubic bool HasDescription { get; set; }
    
    public string Title { get; set; }

    [RequireIfTreu("HasDescription")]
    public string Description { get; set; }
}
```
----
#### Check If Specific Property Has Specific Value
usage :
```
public class TestViewModel {

    pubic SampleEnum Status { get; set; }
    
    public string Title { get; set; }

    [RequireIfTreu("Status", SampleStatus.RequireDescription)]
    public string Description { get; set; }
}
```
