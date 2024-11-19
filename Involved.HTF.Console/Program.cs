using Involved.HTF.Common;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;

#region Token & Login
HackTheFutureClient _client = new HackTheFutureClient();
string _teamname = "Black-Viper";
string _password = "6e552d33-6a87-4ec7-b7ac-861f58419098";

_client.Login(_teamname, _password).Wait();

string _token = _client.DefaultRequestHeaders.Authorization.Parameter;

Debug.WriteLine($"Logged in as {_teamname} with token {_token}");
_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
#endregion

#region E1 Aqualon Prime's Deep Mission
//try
//{
//    string _start = await _client.GetStringAsync("/api/a/easy/start");
//    Debug.WriteLine($"Response: Started");

//    var _result = await _client.GetAsync("/api/a/easy/puzzle");
//    var diveCommand = await _result.Content.ReadFromJsonAsync<DiveCommands>();
//    var _commands = diveCommand.Commands.Split(',');
//    var commandEntries = _commands.Select(c => c.Split(' ')).ToList();
//    int _location = 0;
//    int _depth = 0;
//    int _forward = 0;
//    foreach ( var commandEntry in commandEntries)
//    {
//        switch (commandEntry[0])
//        {
//            case "Down":
//                _depth += int.Parse(commandEntry[1]);
//                break;
//            case "Up":
//                _depth -= int.Parse(commandEntry[1]);
//                break;
//            case "Forward":
//                _forward += int.Parse(commandEntry[1]);
//                _location += (_depth* int.Parse(commandEntry[1]));
//                break;
//        }
//    }
//    var total = _location * _forward;
//    var dive = await _client.PostAsJsonAsync("/api/a/easy/puzzle", total);
//    Debug.WriteLine(await dive.Content.ReadAsStringAsync());
//}
//catch (HttpRequestException ex)
//{
//    Debug.WriteLine($"Request error: {ex.Message}");
//}
//public class DiveCommands
//{
//    public string Commands { get; set; }
//}
#endregion
#region B1 The secret Code of the Cosmic Stone 
//string _startB1 = await _client.GetStringAsync("/api/b/easy/start");
//Debug.WriteLine($"Start challenge: Started B1");

//var _alphabet = await _client.GetStringAsync("/api/b/easy/alphabet");
//_alphabet = _alphabet.Replace("{", "").Replace("}", "").Replace(""", "");
//Debug.WriteLine($"Alphabet: {_alphabet}");

//var _codedMessage = await _client.GetFromJsonAsync<CodedMessage>("/api/b/easy/puzzle");
//Debug.WriteLine($"Coded message: {_codedMessage.AlienMessage}");

//var _letters = _alphabet.Split(",");
//var _letterList = _letters.Select(l => l.Split(":")).ToList();

//string _decodedMessage = "";
//foreach (var charater in _codedMessage.AlienMessage)
//{
//    bool _isLetter = false;

//    if (charater.ToString() == " ")
//    {
//        _decodedMessage += " ";
//        continue;
//    }

//    foreach (var letter in _letterList)
//    {
//        if (charater.ToString() == letter[1])
//        {
//            _decodedMessage += letter[0];
//            _isLetter = true;
//            break;
//        }
//    }

//    if (!_isLetter)
//    {
//        _decodedMessage += charater;
//    }

//}
//Debug.WriteLine($"Decoded message: {_decodedMessage}");
//var _result = await _client.PostAsJsonAsync("/api/b/easy/puzzle", _decodedMessage);
//Debug.WriteLine(await _result.Content.ReadAsStringAsync());
//public class CodedMessage
//{
//    public string AlienMessage { get; set; }
//}
#endregion
#region M2 The battle of Nova Centauri
await _client.GetAsync("/api/a/medium/start");
var winners = new Winners();
var battle = await _client.GetFromJsonAsync<Battle>("/api/a/medium/sample");
while(battle.TeamA.Count > 0 && battle.TeamB.Count > 0)
{
    if(battle.TeamA.First().Speed >= battle.TeamB.First().Speed)
    {
        while (true)
        {
            battle.TeamB.First().Health -= battle.TeamA.First().Strength;
            if(battle.TeamB.First().Health <= 0)
            {
                battle.TeamB.RemoveAt(0);
                break;
            }
            battle.TeamA.First().Health -= battle.TeamB.First().Strength;
            if (battle.TeamA.First().Health <= 0)
            {
                battle.TeamA.RemoveAt(0);
                break;
            }
        }
    }
    else
    {
        while (true)
        {
            battle.TeamA.First().Health -= battle.TeamB.First().Strength;
            if (battle.TeamA.First().Health <= 0)
            {
                battle.TeamA.RemoveAt(0);
                break;
            }
            battle.TeamB.First().Health -= battle.TeamA.First().Strength;
            if (battle.TeamB.First().Health <= 0)
            {
                battle.TeamA.RemoveAt(0);
                break;
            }
        }
    }
}
if(battle.TeamA.Count == 0)
{
    winners.winningTeam = "TeamB";
    winners.remainingHealth = battle.TeamB.Sum(a => a.Health);
}
else
{
    winners.winningTeam = "TeamA";
    winners.remainingHealth = battle.TeamA.Sum(a => a.Health);
}
var result = await _client.PostAsJsonAsync("/api/a/medium/sample", winners);
Debug.WriteLine(await result.Content.ReadAsStringAsync());
public class Winners
{
    public string winningTeam { get; set; }
    public int remainingHealth { get; set; }
}
public class Battle
{
    public List<Alien> TeamA { get; set; }
    public List<Alien> TeamB { get; set; }
}
public class Alien
{
    public int Strength { get; set; }
    public int Speed { get; set; }
    public int Health { get; set; }
}
#endregion
#region B2 Zyphora: The Waiting World
//string _startB2 = await _client.GetStringAsync("/api/b/medium/start");
//Debug.WriteLine($"Start challenge: Started B2");

//var _time = await _client.GetFromJsonAsync<TimeResult>("/api/b/medium/sample");
//Debug.WriteLine($"Time: {_time.SendDateTime}, travelspeed: {_time.TravelSpeed}, travel distance: {_time.Distance}, day lenght:{_time.DayLength}");

//public class TimeResult
//{
//    public DateTime SendDateTime { get; set; }
//    public int TravelSpeed { get; set; }
//    public int Distance { get; set; }
//    public int DayLength { get; set; }
//}

//public class CalculateTime(TimeResult result)
//{

//}
#endregion