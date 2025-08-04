# Introduction 
This repository contains libraries that are used in the .NET applications of the federal chancellery.
These are written and maintained by the BK-DevOps Team.

# BK.Activities
Holds a schema and model of the ChfActivityType used to transmit "BK-Activities" between their applications.

## Workflow for Modifications on the Schema
- Define the new Version in 'ChfActivityVersion.cs'
- Move the current version of the schema from 'Bk.Activities.Model' to 'Bk.Activities.Model.[your-version]'
- Do the same for the mapping profiles ('Bk.Activities.Mapping.Profiles')
- Create a new version of the schema and mappers in their respective root folders (do not forget to specify the version in the .xsd).
- Modify XSD and mappers to represent your changes
- generate the C# classes using the GenerateClasses.bat in Bk.Activities/Model -> '''[...]src\Bk.Activities\Bk.Activities\Model> .\GenerateClasses.bat'''
- specify the namespace 'namespace Bk.Activities.Model' on the generated classes




 