#!/bin/bash

RED='\033[0;31m'
NO_COLOR='\033[0m' # No Color

dotnet ef database update --project ./FinanceOne.DataAccess/ --startup-project ./FinanceOne.WebApi
