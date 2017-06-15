# nancy-simple-app
Simple Nancy app login and logout practice

Register, login, logout, index page and database using SQLite

Note that this project is develop using Mono in Ubuntu. Also, I modified the .csproj file to copy Views folders to debug folder (publish folder):

    <Target Name="AfterBuild" Condition=" '$(Configuration)' == 'Debug' ">
        <!-- needed to deply views folder -->
        <Exec Command="cp -a  Views $(OutDir)" />
        <!-- needed to deply database if not newer -->
        <Exec Command="cp -a -u  Database/db.sqlite $(OutDir)" />
    </Target>
