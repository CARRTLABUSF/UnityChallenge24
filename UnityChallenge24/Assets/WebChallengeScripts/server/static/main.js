// WebSocket connection
const socket = new WebSocket("ws://localhost:8080/ws");

const charts = {
  pathXY: createChart('pathXYChart', 'Path (X-Y)', 'X', 'Y'),
  pathXZ: createChart('pathXZChart', 'Path (X-Z)', 'X', 'Z'),
  velocityX: createChart('velocityXChart', 'Velocity (X)', 'Time', 'Velocity'),
  velocityY: createChart('velocityYChart', 'Velocity (Y)', 'Time', 'Velocity'),
  velocityZ: createChart('velocityZChart', 'Velocity (Z)', 'Time', 'Velocity'),
  absoluteVelocity: createChart('absoluteVelocityChart', 'Absolute Velocity', 'Time', 'Velocity'),
  rotationX: createChart('rotationXChart', 'Rotation (X)', 'Time', 'Angle', true),
  rotationY: createChart('rotationYChart', 'Rotation (Y)', 'Time', 'Angle', true),
  rotationZ: createChart('rotationZChart', 'Rotation (Z)', 'Time', 'Angle', true)
};

const rotationCharts = {
  rotationX: initializeRotationChart('rotationXChart', 'rotationXAngle'),
  rotationY: initializeRotationChart('rotationYChart', 'rotationYAngle'),
  rotationZ: initializeRotationChart('rotationZChart', 'rotationZAngle')
};

function initializeRotationChart(canvasId, angleDisplayId) {
  const canvas = document.getElementById(canvasId);
  const angleDisplay = document.getElementById(angleDisplayId);
  const ctx = canvas.getContext('2d');
  const centerX = canvas.width / 2;
  const centerY = canvas.height / 2;

  return {
      ctx,
      centerX,
      centerY,
      angle: 0, // Initial angle
      angleDisplay
  };
}

function drawRotationChart(chart, angle, color) {
  const { ctx, centerX, centerY, angleDisplay } = chart;
  const triangleSize = 40; 

  ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);

  // Draw the compass circle
  ctx.beginPath();
  ctx.arc(centerX, centerY, 60, 0, 2 * Math.PI);
  ctx.strokeStyle = '#333';
  ctx.lineWidth = 1;
  ctx.stroke();

  // Save the context state
  ctx.save();

  // Rotate the canvas around the center
  ctx.translate(centerX, centerY);
  ctx.rotate((angle * Math.PI) / 180); // Convert degrees to radians
  ctx.translate(-centerX, -centerY);

  ctx.beginPath();
  ctx.moveTo(centerX, centerY - triangleSize); // Top vertex
  ctx.lineTo(centerX - triangleSize / 2, centerY + triangleSize / 2); // Bottom left vertex
  ctx.lineTo(centerX + triangleSize / 2, centerY + triangleSize / 2); // Bottom right vertex
  ctx.closePath();
  ctx.fillStyle = color;
  ctx.fill();
  // Restore the context state
  ctx.restore();

  angleDisplay.textContent = `Angle: ${angle.toFixed(2)}°`;
}

// Function to create a CanvasJS chart
function createChart(containerId, title, xAxisTitle, yAxisTitle, isRotation = false) {
  return new CanvasJS.Chart(containerId, {
      title: {
          text: title,
          fontFamily: 'Arial',
          fontSize: 18,
          fontWeight: 'bold'
      },
      axisX: {
          title: xAxisTitle,
          titleFontSize: 14,
          titleFontFamily: 'Arial',
          labelFontSize: 12
      },
      axisY: {
          title: yAxisTitle,
          titleFontSize: 14,
          titleFontFamily: 'Arial',
          labelFontSize: 12
      },
      data: [{
          type: 'line',
          dataPoints: []
      }]
  });
}


socket.onopen = () => {
  console.log("WebSocket connection established");
};

socket.onmessage = (event) => {
  if (event.data instanceof Blob) {
    // Hint: Useful distinction

  } 
  else {
    try {
      const data = JSON.parse(event.data);
      console.log(data)
      //Handle incoming websocket messages

      updateChart(charts.pathXY, data.time, { x: data.posX, y: data.posY });
      updateChart(charts.pathXZ, data.time, { x: data.posX, y: data.posZ });

      updateChart(charts.velocityX, data.time, { x: data.time, y: data.velX });
      updateChart(charts.velocityY, data.time, { x: data.time, y: data.velY });
      updateChart(charts.velocityZ, data.time, { x: data.time, y: data.velZ });
      updateChart(charts.absoluteVelocity, data.time, {x: data.time, y: data.velAbs});  

      drawRotationChart(rotationCharts.rotationX, data.eulerX, 'red');
      drawRotationChart(rotationCharts.rotationY, data.eulerY, 'green');
      drawRotationChart(rotationCharts.rotationZ, data.eulerZ, 'blue');

      
    } catch (err) {
      console.error("Error parsing position data:", err);
    }
  }
};

function updateChart(chart, time, dataPoint) {
  const dataSeries = chart.options.data[0].dataPoints;

  dataSeries.push(dataPoint);

  // Limiting the number of data points to 25
  if (dataSeries.length > 25) {
      dataSeries.shift();
  }
  chart.render();
}
