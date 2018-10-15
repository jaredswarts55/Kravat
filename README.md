# Kravat

Data workflow programming in .net.

# Motivation

Programming websites/apis is often a repetitive process. Scaffolding and current flow programming (example: https://www.totaljs.com/flow/) offer two drastically different solutions. The goal of this framework is to provide a set of tools to visually scaffold an api. It goes beyond simple scaffolding by offering some of the elements that flow programming does. Stitching together service calls to produce a result in the generated file. As an example, [this configuration file](docs/kravatFormat.json) will produce two controllers. The UsersController and a [CompaniesController](docs/CompanyController.cs) with one configured endpoint.

# Configuration Format

The configuration file contains the configured models, the Endpoints (description of a url and what it does), the Workflows (a list of service methods to execute in order), and the Services (Configured Services/Methods that the Workflows use). [Here is an example](docs/kravatFormat.json).

# Goals

-   [x] Allow the generation of the models json using a DLL containing an Entity Framework Context class.
-   [ ] Produce Controllers based on the configured models, services, and workflows. (Partially Complete, only allows one service in a workflow atm)
-   [ ] Create a visual editor to edit the Models, Endpoints, Services, and Workflows.
-   [ ] Create a command line tool that takes in a configuration file and a template file and produces a rendered file.
-   [ ] Create template files for javascript/typescript models and services.
