# Introduction

Steps to build and run the code in VS2019
1. Open command prompt and navigate to any local folder; and give the below command 
2. git clone https://github.com/sharmilabogireddy/UserRegistration.git
3. This will create UserRegistration folder
4. goto UserRegistration folder and click on UserRegistration.sln file
5. This will open the solution in VS2019 or similar VS version (depends on what VS version installed)
6. Press CTRL+F5 to run the porject and open the login screen in browser.

# user Registration


# Login Screen


# Workaround to see the screens (if no DB setup)

1. Navigate to https://localhost:44341/user/login to see the login page
2. Navigate to https://localhost:44341/user/registration to see the registration page

# How to deploy in IIS server

1. In visual studio select the solution explorer and right click on the project name and it will       open the menu, in that menu select publish.
2. Navigate to 'C:\inetpub\wwwroot' and create a folder called 'registration'.
3. Unzip the publish folder into some temp folder.
4. Copy the contents of the 'publish' folder and paste in 'registration' folder.
5. Open IIS manager.
6. Right click on 'Sites' and click on 'Add Website..'.
7. In 'Add Website' dialog box provide the following details
        Site Name : registration (just for example but it could be anything related name)
        Physical Path : select the path from 'C:\inetpub\wwwroot\registration'
        Binding 
            Port : 8091 (for example)
    and click 'Ok'.
8. Make sure the above site is in running state.
9. Open browser and give the url 'http://localhost:8091'
4. 
