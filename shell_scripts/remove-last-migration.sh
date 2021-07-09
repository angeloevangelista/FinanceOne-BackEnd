#!/bin/bash

dotnet ef migrations remove --project ./FinanceOne.DataAccess/ --startup-project ./FinanceOne.WebApi
