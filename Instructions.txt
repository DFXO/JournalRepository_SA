
Database
--------

The database is created using Code-First technique of the Entity Framework.

	1. Configuration String:
	-----------------------
		The Configuration String for the database is configured in the respective Web.Config and 
		App.Config files of Journals.Web:

			<add name="JournalsDB" connectionString="Data Source=localhost;Initial 
			Catalog=JournalsDB;Integrated Security=True;" providerName="System.Data.SqlClient" />
   
	2. Creating UserProfile Tables & Seeding Users:
	-----------------

		1. Ensure all the NuGet Packages are available and installed:
			a. Right-click the solution and select "Manage NuGet Packages for Solution". If any packages 
				are required to be installed, it will indicate at the top of the dialog box. "Install" the 
				missing packages. (This may take a while.)
				
		2. Build the Solution and run the Journals.Web application. To run the Journals.Web Appliation:
			a. Right-click the Journals.Web project and select "Set as StartUp Project"
			b. Hit F5 or Right-click Journals.Web and select Debug > Start a new instance

		3. Once the command run successfully, this will create database, UserProfile related tables and 
the following users and roles:
			a. UserName: pappu, 	Password: Passw0rd, Role: Publisher
			b. UserName: pappy, 	Password: Passw0rd, Role: Subscriber
			c. UserName: daniel, 	Password: Passw0rd, Role: Publisher
			d. UserName: andrew, 	Password: Passw0rd, Role: Subscriber
			e. UserName: serge, 	Password: Passw0rd, Role: Subscriber
			f. UserName: harold, 	Password: Passw0rd, Role: Publisher
			
Running the application:
------------------------
		
		In the solution folder, you will find Journals.sln. Opening that in Visual Studio, will open 
the Solution and load all the projects.

		1. Ensure all the NuGet Packages are available and installed:
			a. Right-click the solution and select "Manage NuGet Packages for Solution". If any packages 
are required to be installed, it will indicate at the top of the dialog box. "Install" the 
missing packages.
			
		2. Perform the steps mentioned in "Database" section of this document, if you haven't already done 
that.
			
		3. To run the Journals.Web Appliation:
			a. Right-click the Journals.Web project and select "Set as StartUp Project"
			b. Hit F5 or Right-click Journals.Web and select Debug > Start a new instance
			
		This will run the Journals.Web application and launch the hompepage. Once on homepage, you can click 
on the "Publisher" section, login as "daniel" and get started.
		
New Features:
-------------

The application is updated to provide the feature of creating montly "Issues". Once the Journal is created, you can see three new buttons on the
row - Edit, Delete, Issues, Functionalilty of each are as explained below:

1. Issues: If you want to create new issues for the Journals, click on "Issues". You will be navigated to issues page. Here you will see the list of 
all the Issues associated with the Journal. Here you will see the option to "Create Issues", you can create new issues from here. We have added a validation
to allow only one issue for month

2. Edit - This will allow user to edit the details of Journal.

3. Delete - This will allow user to delete the Journal along with any of the "Issues" associated with it.

 
		
		
			
				
			
			
		
		

		
		
 






