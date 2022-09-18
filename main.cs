using (var house = new House(numberOfRoom: 4))
{
    house.SetRoomName(0, "Rest");
    house.SetRoomName(1, "Living");
    house.SetRoomName(2, "Kitchen");
    house.SetRoomName(3, "Bed");

    var chair = new Chair();
    var table = new Table();
    house.AddFurnitureToRoom("Living", chair);
    house.AddFurnitureToRoom("Living", table);

    var bed = new Bed();
    house.AddFurnitureToRoom("Bed", bed);

    WriteLine(house);
}

class House : IDisposable
{
    private Room[] _rooms;
    private int _numberOfRoom;

    //A house has a composition relationship with a room
    public House(int numberOfRoom)
    {
        _rooms = new Room[numberOfRoom];
        for (var i = 0; i < numberOfRoom; i++)
        {
            _rooms[i] = new Room();
        }
        _numberOfRoom = numberOfRoom;
        WriteLine("A house is constructed");
    }

    public void SetRoomName(int roomIndex, string roomName)
    {
        if (roomIndex < _numberOfRoom)
        {
            _rooms[roomIndex].Name = roomName;
        }
    }

    //A room has a dependency relationship with a furniture 
    public void AddFurnitureToRoom(string roomName, Furniture furniture)
    {
        for (var i = 0; i < _numberOfRoom; i++)
        {
            if (_rooms[i].Name == roomName)
            {
                _rooms[i].AddFurniture(furniture);
                break;
            }
        }
    }

    public override string ToString()
    {
        var summary = string.Empty;
        for (var i = 0; i < _numberOfRoom; i++)
        {
            summary += $"{_rooms[i].Name} room\n";
            var furnitures = _rooms[i].Furnitures;
            if (furnitures.Any())
            {
                foreach (var furniture in furnitures)
                {
                    summary += $" - {furniture.Name}\n";
                }
            }
        }
        return summary;
    }

    public void Dispose()
    {
        for (var i = 0; i < _numberOfRoom; i++)
        {
            WriteLine($"The {_rooms[i].Name} room is disposing");
            _rooms[i].Dispose();
        }
        WriteLine("The house is deconstructed");
    }
 
    class Room : IDisposable
    {
        public string Name { get; set; }
        public IList<Furniture> Furnitures { get { return _furnitures; } }
        private IList<Furniture> _furnitures = new List<Furniture>();
        public Room()
        {
            WriteLine("A room is constructed");
        }

        //A room has a aggregation relationship with a furniture
        public void AddFurniture(Furniture furniture)
        {
            _furnitures.Add(furniture);
        }

        public void Dispose()
        {
            WriteLine("A room is deconstructed");
        }
    }
}

class Furniture
{
    public string Name { get; set; }
}

//A chair has an inheritance relationship with a furniture
class Chair : Furniture
{
    public Chair()
    {
        Name = "Chair";
    }
}

class Table : Furniture
{
    public Table()
    {
        Name = "Table";
    }
}

class Bed : Furniture
{
    public Bed()
    {
        Name = "Bed";
    }
}