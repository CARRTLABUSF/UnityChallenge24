// WebSocket connection
const socket = new WebSocket("ws://localhost:8080/ws");

const charts = {}; 

const createChart = (canvasId, label, color, labels = [], data = []) => {
    const ctx = document.getElementById(canvasId).getContext("2d");

    // Check if incoming data is array
    labels = Array.isArray(labels) ? labels : [labels];
    data = Array.isArray(data) ? data : [data];

    // Check if chart already exists
    if (!charts[canvasId]) {
      charts[canvasId] = new Chart(ctx, {
          type: "line",
          data: {
              labels: [...labels], 
              datasets: [{
                  label: label,
                  data: [...data], 
                  borderColor: color,
                  borderWidth: 2,
              }]
          },
          options: {
              responsive: true,
              maintainAspectRatio: false,
              animations: {
                  radius: {
                      duration: 400,
                      easing: "linear",
                      loop: (context) => context.active,
                  },
              },
          },
      });
  }
  // Updating data
    else {
      
      const chart = charts[canvasId];

      chart.data.labels.push(...labels);
      chart.data.datasets[0].data.push(...data);

      // Limit to last 10 points
      if (chart.data.labels.length > 10) {
          chart.data.labels.shift();
      }
      chart.update();

    }

  return charts[canvasId];
}

socket.onmessage = (event) => {
  if (event.data instanceof Blob) {
    // Hint: Useful distinction
  } 
  else {
    try {
      const data = JSON.parse(event.data);
      console.log(data)
      
      posX = data.posX.toPrecision(3),   // X-coordinate of the object's position
      posY = data.posY.toPrecision(3),   // Y-coordinate of the object's position
      posZ = data.posZ.toPrecision(3),   // Z-coordinate of the object's position
      eulerX = data.eulerX.toPrecision(3), // X-angle (roll) in Euler coordinates
      eulerY = data.eulerY.toPrecision(3), // Y-angle (pitch) in Euler coordinates
      eulerZ = data.eulerZ.toPrecision(3), // Z-angle (yaw) in Euler coordinates
      velX = data.velX.toPrecision(3),   // X-component of velocity
      velY = data.velY.toPrecision(3),   // Y-component of velocity
      velZ = data.velZ.toPrecision(3),   // Z-component of velocity
      velAbs = data.velAbs.toPrecision(3), // Absolute velocity magnitude
      time = data.time.toPrecision(3)    // Timestamp of the data

      // Creating chart instances
      chartPosXY = createChart("posXY","Position (X/Y)", "#e282de", posY, posX);
      chartPosXZ = createChart("posXZ","Position (X/Z)", "#e282de", posZ, posX);
      chartVelX = createChart("velX","Velocity (X)", "red", time, velX);
      chartVelY = createChart("velY","Velocity (Y)", "red", time, velY);
      chartVelZ = createChart("velZ","Velocity (Z)", "red", time, velZ);
      chartVelAbs = createChart("velAbs","Absolute Velocity", "red", time, velAbs);
      chartEulerX = createChart("eulerX","Rotation (X)", "#325aa8", time, eulerX ? eulerX : 0);
      chartEulerY = createChart("eulerY","Rotation (Y)", "#325aa8", time, eulerY ? eulerY : 0);
      chartEulerZ = createChart("eulerZ","Rotation (Z)", "#325aa8", time, eulerZ ? eulerZ : 0);

    } catch (err) {
      console.error("Error parsing position data:", err);
    }
  }
}
  
  socket.onopen = () => {
    console.log("WebSocket connection established");
  };
  