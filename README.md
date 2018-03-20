# ServerMod2
ServerMod2 is a server side plugin system with a bunch of additional configuration options, bug fixes, security patches and some optimisations built in.

The latest release can be found here: [Release link](https://github.com/Grover-c13/Smod2/releases/latest)

## Discord
You can join our discord here: https://discord.gg/8nvmMTr

## ServerMod Installation:
To install:
1. Navigate to your SCP Secret Lab folder.
2. Go into SCSL_Data/Managed/
3. Make a backup of Assembly-CSharp.dll
4. Replace Assembly-CSharp.dll with the one in the releases tab.

## Server Name Variables
Currently supported variables (place in your servers name):
- $player_count (current number of connected players) EG: "$player_count playing!"
- $port (the port of the current server) EG: "Welcome to SCPServer.com:$port"
- $ip (the ip of the server) EG: "Welcome to SCPServer.com [$ip:$port]"
- $full_player_count (will display player count as $player_count/$max_player_count or FULL if there are $max_player_count players) EG: "Server.com $full_player_count"
- $number (will display the number of the instance, assuming youre using default ports, this works by subtracting 7776 from the port (so $number will = 1 for the first server, #2 for the second)
- $lobby_id (debugging to print the lobby_id)
- $version (version of the game)
- $max_players (max amount of players in the config)
- $scp_alive - number of alive SCPS.
- $scp_start - number of SCPs at start of the round.
- $scp_counter - prints $scp_alive/$scp_start
- $scp_dead - number of dead scps.
- $scp_zombies - current number of zombies.
- $classd_escape - how many class ds have escaped.
- $classd_start - the amount of starting class ds.
- $classd_counter - $classd_escape/$classd_counter.
- $scientists_escape - The number of scientists to escape so far.
- $scientists_start - the amount of starting scientists
- $scientists_counter - $scientists_escape/$scientist_start.
- $scp_kills - number of people killed by scps.
- $warhead_detonated - prints ☢ WARHEAD DETONATED ☢ if its gone off.

Example:
![player count](https://user-images.githubusercontent.com/1520101/36029888-04689b5c-0de0-11e8-81cd-b1d458caf7e9.png)

## Config Additions
Type Info:
- Boolean: True or False value
- Integer: A number without decimals
- List: A list with items separated by ","
- Seconds: Time in seconds, usually a value of -1 disables the feature

### General
Config Option | Value Type | Default Value | Description
--- | :---: | :---: | ---
max_players | Integer | 20 | The max amount of players per server
force_disable_enable | Boolean | False | Overrides game's default ban value with chosen values (**USE OF THIS IS NOT RECOMMENDED**)
item_cleanup | Seconds | -1 | Cleans up items after the specified amount of time
nickname_filter | List | **Empty** | Automatically kicks anyone who's nickname contains anything in this list
show_on_serverlist | Boolean | True | If your server is verified, this shows it on the server list
server_frame_rate | Integer | 60 | The framerate that a server runs at
allow_incompatible | Boolean | False | Allow the server to run an incompatible version of ServerMod
sm_debug | Boolean | False | Print more verbose debug messages for debugging
sm_server_name | String | **Dynamic** | server name in a separate option, defaults to the value of server_name (You'd use this if you don't want variables showing up in your server name when ServerMod isn't working)
sm_tracking | Boolean | True | Appends the ServerMod version to your server name, this is for tracking how many servers are running ServerMod
allow_project_manager_remote_admin | Boolean | False | Allow SCP: SL project managers to use Remote Admin
allow_scpsl_staff_to_use_remoteadmin | Boolean | False | Allow SCP: SL staff to use Remote Admin
allow_scpsl_beta_tester_to_use_remoteadmin | Boolean | False | Allow SCP: SL beta testers to use Remote Admin
allow_scpsl_patreon_to_use_remoteadmin | Boolean | False | Allow SCP: SL patrons to use Remote Admin
afk_kick | Seconds | -1 | Kicks players who haven't moved in a specified amount of time

### Warhead Options
Config Option | Value Type | Default Value | Description
--- | :---: | :---: | ---
nuke_disable_cooldown | Seconds | 0 | Stop the nuke from being spammed, this will stop the nuke arm switch from being disabled until this has elapsed
auto_warhead_start | Seconds | -1 | Automatically activated the nuke after the specified amount of time has elapsed (-1 disables this feature)
auto_warhead_start_lock | Boolean | False | Automatically prevents the warhead detonation from being cancelled when it's automatically started
unlock_nuke_door_on_detonate | Boolean | True | Makes all doors openable without a keycard after the nuke has detonated

### Class Based
Config Option | Value Type | Default Value | Description
--- | :---: | :---: | ---
no_scp079_first | Boolean | True | Computer (SCP-079) will never be the first scp in a game
173_door_starting_cooldown | Seconds | 25 | The time before SCP-173's door can be opened
SCP106_cleanup | Boolean | False | Stops items and ragdolls from spawning in the pocket dimension
maximum_MTF_respawn_amount | Integer | 15 | The maximum amount of MTF that can be respawned in a single respawn wave
SCP049_HP | Integer | 1200 | Sets the starting HP for SCP-049
SCP049-2_HP | Integer | 400 | Sets the starting HP for SCP-049-2
SCP079_HP | Integer | 100 | Sets the starting HP for SCP-079
SCP096_HP | Integer | 2000 | Sets the starting HP for SCP-096
SCP106_HP | Integer | 700 | Sets the starting HP for SCP-106
SCP173_HP | Integer | 2000 | Sets the starting HP for SCP-173
SCP457_HP | Integer | 700 | Sets the starting HP for SCP-457
CLASSD_HP | Integer | 100 | Sets the starting HP for Class Ds
SCIENTIST_HP | Integer | 100 | Sets the starting HP for Scientists
CI_HP | Integer | 100 | Sets the starting HP for Chaos Insurgency
NTFG_HP | Integer | 100 | Sets the starting HP for NTF Guards
NTFSCIENTIST_HP | Integer | 120 | Sets the starting HP for NTF Scientists
NTFL_HP | Integer | 120 | Sets the starting HP for NTF Lieutenants
NTFC_HP | Integer | 150 | Sets the starting HP for NTF Commanders
SCP049_DISABLE | Boolean | False | Disables SCP-049
SCP079_DISABLE | Boolean | True | Disables SCP-079
SCP096_DISABLE | Boolean | False | Disables SCP-096
SCP106_DISABLE | Boolean | False | Disables SCP-106
SCP173_DISABLE | Boolean | False | Disables SCP-173
SCP457_DISABLE | Boolean | True | Disables SCP-457
SCP049_AMOUNT | Integer | 1 | Max amount of SCP-049 that can be spawned in randomly
SCP079_AMOUNT | Integer | 1 | Max amount of SCP-079 that can be spawned in randomly
SCP096_AMOUNT | Integer | 1 | Max amount of SCP-096 that can be spawned in randomly
SCP106_AMOUNT | Integer | 1 | Max amount of SCP-106 that can be spawned in randomly
SCP173_AMOUNT | Integer | 1 | Max amount of SCP-173 that can be spawned in randomly
SCP457_AMOUNT | Integer | 1 | Max amount of SCP-457 that can be spawned in randomly

### Risky IP Checker
#### Before using this, make sure you check out the API that's used, https://getipintel.net/
Config Option | Value Type | Default Value | Description
--- | :---: | :---: | ---
kick_risky_ips | Boolean | False | Enables/Disables Risky IP Checker (Uses https://getipintel.net/)
trusted_ips_reset_every | Integer | 10 | The number of rounds until the cached IPs are cleared
kick_risky_ips_ratelimit | Seconds | 30 | The seconds between requests (CHECK https://getipintel.net/#API)
kick_risky_ips_email | String | **Empty** | Your email, this is used in requests
kick_risky_ips_subdomain | String | check | If you get a custom subdomain for https://getipintel.net/, use it here
kick_risky_ips_at_percent | Integer | 95 | The percentage of suspicion to kick a player
ban_risky_ips_at_percent | Integer | 100 | The percentage of suspicion to ban a player

### Smart Class Picker
Config Option | Value Type | Default Value | Description
--- | :---: | :---: | ---
smart_class_picker | Boolean | False | Enables/Disables Smart Class Picker
smart_class_picker_starting_weight | Integer | 5 | The weight a class starts out with
smart_class_picker_weight_limit | Integer | 10 | The maximum weight a class can have
smart_class_picker_class_<Class #>_weight_decrease | Integer | **Dynamic** | The amount a weight goes down when a player plays the specified class, the default value is dynamic based on which team and class the player is
smart_class_picker_class_<Class #>_weight_increase | Integer | **Dynamic** | The amount a weight goes up when the player isn't the specified class, the default value is dynamic based on which team and class the player is
smart_class_picker_team_<Team #>_weight_decrease | Integer | **Dynamic** | The amount the weight for each class on a team goes down when a player plays on the specified team, the default value is dynamic based on which team and class the player is
smart_class_picker_team_<Team #>_weight_increase | Integer | **Dynamic** | The amount the weight for each class on a team goes up when the player isn't on the specified team, the default value is dynamic based on which team and class the player is


#### Default Functionality
- Every class gets +1 weight except for the class the player is chosen to be or the chosen class is NTF
- If the player is chosen to be NTF, the chosen class gets -4 weight and every other NTF class gets -2 weight
- If the player is chosen to be SCP, the chosen class gets -3 weight and every other SCP class gets -2 weight
- If the player is chosen to be Class D, Class D gets -3 weight
- If the player is chosen to be any other class, the chosen class gets -2 weight

##

Place any suggestions/problems in issues!

Thanks & Enjoy.

