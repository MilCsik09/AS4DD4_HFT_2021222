let brands = []
let connection = null;
let brandIdToUpdate = -1;


setupSignalR();



getdata();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:21845/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BrandCreated", (user, message) => {
        getdata();
    });

    connection.on("BrandDeleted", (user, message) => {
        getdata();
    });

    connection.on("BrandUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


async function getdata() {
    await fetch('http://localhost:21845/Brand')
        .then(x => x.json())
        .then(y => {
            brands = y;
            console.log(y);
            display();
        });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    brands.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button>` +
            "</td></tr > ";
    });
}


function create() {
    let name = document.getElementById('brandname').value;

    fetch('http://localhost:21845/brand', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                Name: name,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

    

}


function remove(id) {
    fetch('http://localhost:21845/brand/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function update() {
    addHidden();
    let name = document.getElementById('brandtoupdate').value;

    fetch('http://localhost:21845/brand', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name,
                Id: brandIdToUpdate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}


function addHidden() {
    let element = document.getElementById('updateform');
    element.classList.add('hidden');
    element.classList.remove('form');
}


function removeHidden() {
    let element = document.getElementById('updateform');
    element.classList.remove('hidden');
    element.classList.add('form');
}


function showupdate(id) {
    document.getElementById('brandtoupdate').value = brands.find(a => a['id'] == id)['name'];
    removeHidden();
    brandIdToUpdate = id;

}

