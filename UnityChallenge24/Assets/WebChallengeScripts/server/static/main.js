// WebSocket connection
const socket = new WebSocket("ws://localhost:8080/ws");
socket.binaryType = "arraybuffer"; // Ensure binary data is received
let timeLabels = [];
let posXZ = { x: [], z: [] };
let posXY = { x: [], y: [] };
let velData = { x: [], y: [], z: [], abs: [] };
let rotationX = 0;
let rotationY = 0;
let rotationZ = 0;


const canvasX = document.getElementById("rotationX");
const ctxX = canvasX.getContext("2d");
const canvasY = document.getElementById("rotationY");
const ctxY = canvasY.getContext("2d");
const canvasZ = document.getElementById("rotationZ");
const ctxZ = canvasZ.getContext("2d");

// Set canvas sizes
canvasX.width = 150; canvasX.height = 150;
canvasY.width = 150; canvasY.height = 150;
canvasZ.width = 150; canvasZ.height = 150;

// Function to draw a rotating triangle
function drawTriangle(ctx, angle, color) {
    ctx.clearRect(0, 0, 150, 150); // Clear the canvas
    ctx.save();
    ctx.translate(75, 75); // Move it to center of canvas
    ctx.rotate(angle * Math.PI / 180); // Rotate in correct direction
    ctx.beginPath();
    ctx.moveTo(0, -40); 
    ctx.lineTo(-30, 30);
    ctx.lineTo(30, 30); 
    ctx.closePath();
    ctx.fillStyle = color;
    ctx.fill();
    ctx.restore();
}

// Initialize Chart.js graphs
const createChart = (ctx, color) => new Chart(ctx, {
    type: 'line',
    data: { labels: timeLabels, datasets: [{ label:'', data: [], borderColor: color, fill: false }] },
    options: { responsive: true, plugins: {
        legend: { display: false } // Hides both the label & the color box
    },scales: { x: { display: true } } }
});

const pathXZChart = createChart(document.getElementById('pathXZ').getContext('2d'),  "red");
const pathXYChart = createChart(document.getElementById('pathXY').getContext('2d'), "blue");
const velocityXChart = createChart(document.getElementById('velocityX').getContext('2d'),"red");
const velocityYChart = createChart(document.getElementById('velocityY').getContext('2d'), "green");
const velocityZChart = createChart(document.getElementById('velocityZ').getContext('2d'), "red");
const velocityAbsChart = createChart(document.getElementById('velocityAbs').getContext('2d'), "purple");

// WebSocket: Handle incoming data
socket.onmessage = function(event) {
    try {
        if (event.data instanceof ArrayBuffer) {
            const blob = new Blob([event.data], { type: "image/png" });
            const imageUrl = URL.createObjectURL(blob);
            document.getElementById("videoFeed").src = imageUrl;
        }
        else{
        const data = JSON.parse(event.data);
        const MAX_POINTS = 10;
        // console.log(data);

        [timeLabels, posXZ.x, posXZ.z, posXY.x, posXY.y, velData.x, velData.y, velData.z, velData.abs].forEach(arr => {
            if (arr.length > MAX_POINTS) arr.splice(0,1);
        });

        // Update position data
        timeLabels.push(data.time.toFixed(2));
        posXZ.x.push(data.posX);
        posXZ.z.push(data.posZ);
        posXY.x.push(data.posX);
        posXY.y.push(data.posY);
        
        // Update velocity data
        velData.x.push(data.velX);
        velData.y.push(data.velY);
        velData.z.push(data.velZ);
        velData.abs.push(data.velAbs);
        
        // Update Charts
        pathXZChart.data.labels = [...timeLabels];
        pathXZChart.data.datasets[0].data = posXZ.z;
        pathXZChart.update();

        pathXYChart.data.labels = [...timeLabels];
        pathXYChart.data.datasets[0].data = posXY.y;
        pathXYChart.update();
        
        velocityXChart.data.labels = [...timeLabels];
        velocityXChart.data.datasets[0].data = velData.x;
        velocityXChart.update();

        velocityYChart.data.labels = [...timeLabels];
        velocityYChart.data.datasets[0].data = velData.y;
        velocityYChart.update();

        velocityZChart.data.labels = [...timeLabels];
        velocityZChart.data.datasets[0].data = velData.z;
        velocityZChart.update();

        velocityAbsChart.data.labels = [...timeLabels];
        velocityAbsChart.data.datasets[0].data = velData.abs;
        velocityAbsChart.update();

        // Update rotation values
        rotationX = data.eulerX;
        rotationY = data.eulerY;
        rotationZ = data.eulerZ;
        // Setting Inner Text to show the respective rotation angle 
        document.getElementById("rotationXLabel").innerText = rotationX.toFixed(1);
        document.getElementById("rotationYLabel").innerText = rotationY.toFixed(1);
        document.getElementById("rotationZLabel").innerText = rotationZ.toFixed(1);

        drawTriangle(ctxX, rotationX, "red");
        drawTriangle(ctxY, rotationY, "green");
        drawTriangle(ctxZ, rotationZ, "blue");
    }
        
    } catch (error) {
        console.error("Error processing WebSocket message:", error);
    }
};
