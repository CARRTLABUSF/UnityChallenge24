const socket = new WebSocket("ws://localhost:8080/ws");

let pathData = { labels: [], xz: [], xy: [] };
let velocityData = { labels: [], vx: [], vy: [], vz: [], abs: [] };
let rotationData = { roll: 0, pitch: 0, yaw: 0 };

// Function to create a Chart
function createChart(ctx, datasetLabels, datasetRefs, colors) {
    return new Chart(ctx, {
        type: "line",
        data: {
            labels: [],
            datasets: datasetLabels.map((label, index) => ({
                label: label,
                data: datasetRefs[index],
                borderColor: colors[index],
                borderWidth: 2,
                fill: false,
                tension: 0.1
            }))
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            animation: false,
            scales: {
                x: { display: true },
                y: { display: true }
            }
        }
    });
}

// Initialize Charts
document.addEventListener("DOMContentLoaded", () => {
    pathXZChart = createChart(document.getElementById("pathXZ"), ["Path X-Z"], [pathData.xz], ["red"]);
    pathXYChart = createChart(document.getElementById("pathXY"), ["Path X-Y"], [pathData.xy], ["blue"]);
    absoluteVelocityChart = createChart(document.getElementById("absoluteVelocity"), ["Absolute Velocity"], [velocityData.abs], ["black"]);
    velocityXChart = createChart(document.getElementById("velocityX"), ["Velocity X"], [velocityData.vx], ["red"]);
    velocityYChart = createChart(document.getElementById("velocityY"), ["Velocity Y"], [velocityData.vy], ["green"]);
    velocityZChart = createChart(document.getElementById("velocityZ"), ["Velocity Z"], [velocityData.vz], ["blue"]);
});

// WebSocket Event Handlers
socket.onopen = () => console.log("Connected to WebSocket");

socket.onmessage = (event) => {
    try {
        let data = JSON.parse(event.data);
        console.log("⏳ Received Data:", data);

        // Extract values safely
        let posX = data.position?.x ?? 0;
        let posY = data.position?.y ?? 0;
        let posZ = data.position?.z ?? 0;

        let velX = data.velocity?.x ?? 0;
        let velY = data.velocity?.y ?? 0;
        let velZ = data.velocity?.z ?? 0;

        let eulerX = data.rotation?.x ?? 0;
        let eulerY = data.rotation?.y ?? 0;
        let eulerZ = data.rotation?.z ?? 0;

        let timestamp = new Date().toLocaleTimeString();

        // Ensure the timestamp is added correctly
        if (!pathData.labels.includes(timestamp)) {
            pathData.labels.push(timestamp);
            velocityData.labels.push(timestamp);
        }

        // Update Path Data
        pathData.xz.push(posZ);
        pathData.xy.push(posY);
        if (pathData.labels.length > 20) pathData.labels.shift();
        if (pathData.xz.length > 20) pathData.xz.shift();
        if (pathData.xy.length > 20) pathData.xy.shift();

        // Update Velocity Data
        let absVelocity = Math.sqrt(velX ** 2 + velY ** 2 + velZ ** 2);
        velocityData.vx.push(velX);
        velocityData.vy.push(velY);
        velocityData.vz.push(velZ);
        velocityData.abs.push(absVelocity);

        if (velocityData.labels.length > 20) velocityData.labels.shift();
        if (velocityData.vx.length > 20) velocityData.vx.shift();
        if (velocityData.vy.length > 20) velocityData.vy.shift();
        if (velocityData.vz.length > 20) velocityData.vz.shift();
        if (velocityData.abs.length > 20) velocityData.abs.shift();

        rotationData.roll = eulerX;
        rotationData.pitch = eulerY;
        rotationData.yaw = eulerZ;

        // Apply Rotation to Indicators
        document.getElementById("rotationX").style.transform = `rotate(${eulerX * 180 / Math.PI}deg)`;
        document.getElementById("rotationY").style.transform = `rotate(${eulerY * 180 / Math.PI}deg)`;
        document.getElementById("rotationZ").style.transform = `rotate(${eulerZ * 180 / Math.PI}deg)`;

        // Update Charts (Ensure proper updates)
        pathXZChart.data.labels = pathData.labels;
        pathXZChart.data.datasets[0].data = pathData.xz;
        pathXZChart.update();

        pathXYChart.data.labels = pathData.labels;
        pathXYChart.data.datasets[0].data = pathData.xy;
        pathXYChart.update();

        velocityXChart.data.labels = velocityData.labels;
        velocityXChart.data.datasets[0].data = velocityData.vx;
        velocityXChart.update();

        velocityYChart.data.labels = velocityData.labels;
        velocityYChart.data.datasets[0].data = velocityData.vy;
        velocityYChart.update();

        velocityZChart.data.labels = velocityData.labels;
        velocityZChart.data.datasets[0].data = velocityData.vz;
        velocityZChart.update();

        absoluteVelocityChart.data.labels = velocityData.labels;
        absoluteVelocityChart.data.datasets[0].data = velocityData.abs;
        absoluteVelocityChart.update();

    } catch (error) {
        console.error("Error parsing WebSocket data:", error);
    }
};

socket.onerror = (error) => console.log("WebSocket Error:", error);
socket.onclose = () => console.log(" WebSocket Closed");
