const socket = new WebSocket("ws://localhost:8080/ws");

let positionChart, velocityChart, rotationChart;

socket.onopen = () => {
    console.log("✅ WebSocket connection established");
};

socket.onmessage = (event) => {
    console.log("📡 WebSocket received:", event.data); // Debugging

    try {
        const data = JSON.parse(event.data);
        console.log("✅ Parsed Data:", data); // Debugging

        // Update displayed values
        document.getElementById("posX").textContent = data.posX.toFixed(2);
        document.getElementById("posY").textContent = data.posY.toFixed(2);
        document.getElementById("posZ").textContent = data.posZ.toFixed(2);
        document.getElementById("velX").textContent = data.velX.toFixed(2);
        document.getElementById("velY").textContent = data.velY.toFixed(2);
        document.getElementById("velZ").textContent = data.velZ.toFixed(2);
        document.getElementById("eulerX").textContent = data.eulerX.toFixed(2);
        document.getElementById("eulerY").textContent = data.eulerY.toFixed(2);
        document.getElementById("eulerZ").textContent = data.eulerZ.toFixed(2);

        // Update charts
        updateCharts(data);
    } catch (error) {
        console.error("❌ JSON Parse Error:", error, "Received:", event.data);
    }
};

socket.onerror = (error) => {
    console.error("❌ WebSocket error:", error);
};

// Initialize Charts
function initializeCharts() {
    console.log("🟢 Initializing charts...");

    const ctx1 = document.getElementById("positionChart").getContext("2d");
    const ctx2 = document.getElementById("velocityChart").getContext("2d");
    const ctx3 = document.getElementById("rotationChart").getContext("2d");

    positionChart = new Chart(ctx1, {
        type: "line",
        data: { labels: [], datasets: [{ label: "Pos X", borderColor: "red", data: [] },
                                       { label: "Pos Y", borderColor: "green", data: [] },
                                       { label: "Pos Z", borderColor: "blue", data: [] }] },
        options: { responsive: true, scales: { x: { }, y: { beginAtZero: true } } }
    });

    velocityChart = new Chart(ctx2, {
        type: "line",
        data: { labels: [], datasets: [{ label: "Vel X", borderColor: "red", data: [] },
                                       { label: "Vel Y", borderColor: "green", data: [] },
                                       { label: "Vel Z", borderColor: "blue", data: [] }] },
        options: { responsive: true, scales: { x: { }, y: { beginAtZero: true } } }
    });

    rotationChart = new Chart(ctx3, {
        type: "line",
        data: { labels: [], datasets: [{ label: "Euler X", borderColor: "red", data: [] },
                                       { label: "Euler Y", borderColor: "green", data: [] },
                                       { label: "Euler Z", borderColor: "blue", data: [] }] },
        options: { responsive: true, scales: { x: { }, y: { beginAtZero: true } } }
    });

    console.log("✅ Charts initialized");
}

// Update Charts with New Data
function updateCharts(data) {
    const maxDataPoints = 20; // Limit data points to keep graphs readable

    function addData(chart, label, value) {
        if (chart.data.labels.length >= maxDataPoints) {
            chart.data.labels.shift();
            chart.data.datasets.forEach((dataset) => dataset.data.shift());
        }
        chart.data.labels.push(new Date().toLocaleTimeString());
        chart.data.datasets.find((dataset) => dataset.label === label).data.push(value);
        chart.update();
    }

    addData(positionChart, "Pos X", data.posX);
    addData(positionChart, "Pos Y", data.posY);
    addData(positionChart, "Pos Z", data.posZ);

    addData(velocityChart, "Vel X", data.velX);
    addData(velocityChart, "Vel Y", data.velY);
    addData(velocityChart, "Vel Z", data.velZ);

    addData(rotationChart, "Euler X", data.eulerX);
    addData(rotationChart, "Euler Y", data.eulerY);
    addData(rotationChart, "Euler Z", data.eulerZ);
}

// Initialize charts when page loads
document.addEventListener("DOMContentLoaded", initializeCharts);
