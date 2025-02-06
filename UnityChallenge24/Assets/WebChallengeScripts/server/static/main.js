// WebSocket connection
const socket = new WebSocket("ws://localhost:8080/ws");

// Chart Ids
const xyChartId = 'pathXYChart'
const xzChartId = 'pathXZChart'
const velXChartId = 'velocityXChart'
const velYChartId = 'velocityYChart'
const velZChartId = 'velocityZChart'
const absVelChartId = 'absoluteVelocityChart'
const rotXChartId = 'rotationXChart'
const rotYChartId = 'rotationYChart'
const rotZChartId = 'rotationZChart'

// Labels
const pathXYLabel = 'Path(X-Y)'
const pathXZLabel = 'Path(X-Z)'
const xLabel = 'X'
const yLabel = 'Y'
const zLabel = 'Z'
const timeLabel = 'Time'
const velLabel = 'Velocity'
const velXLabel = 'Velocity(X)'
const velYLabel = 'Velocity(Y)'
const velZLabel = 'Velocity(Z)'
const absVelLabel = 'Absolute Velocity'
const charts = {
  pathXY: createChart(xyChartId, pathXYLabel, xLabel, yLabel),
  pathXZ: createChart(xzChartId, pathXZLabel, xLabel, zLabel),
  velocityX: createChart(velXChartId, velXLabel, timeLabel, velLabel),
  velocityY: createChart(velYChartId, velYLabel, timeLabel, velLabel),
  velocityZ: createChart(velZChartId, velZLabel, timeLabel, velLabel),
  absoluteVelocity: createChart(absVelChartId, absVelLabel, timeLabel, velLabel)
};

const rotationCharts = {
  rotationX: initializeRotationChart(rotXChartId, 'rotationXAngle'),
  rotationY: initializeRotationChart(rotYChartId, 'rotationYAngle'),
  rotationZ: initializeRotationChart(rotZChartId, 'rotationZAngle')
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
function createChart(containerId, title, xAxisTitle, yAxisTitle) {
  return new CanvasJS.Chart(containerId, {
      title: {
          text: title,
          fontFamily: 'Arial',
          fontSize: 14,
          fontWeight: 'bold'
      },
      axisX: {
          title: xAxisTitle,
          titleFontSize: 10,
          titleFontFamily: 'Arial',
          labelFontSize: 10
      },
      axisY: {
          title: yAxisTitle,
          titleFontSize: 10,
          titleFontFamily: 'Arial',
          labelFontSize: 10
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
        const blobUrl = URL.createObjectURL(event.data);
        video.src = blobUrl; 
  } 
  else {
    try {
      const data = JSON.parse(event.data);
      console.log(data)
      //Handle incoming websocket messages

      updateChart(charts.pathXY, { x: data.posX, y: data.posY });
      updateChart(charts.pathXZ, { x: data.posX, y: data.posZ });

      updateChart(charts.velocityX, { x: data.time, y: data.velX });
      updateChart(charts.velocityY, { x: data.time, y: data.velY });
      updateChart(charts.velocityZ, { x: data.time, y: data.velZ });
      updateChart(charts.absoluteVelocity, {x: data.time, y: data.velAbs});  

      drawRotationChart(rotationCharts.rotationX, data.eulerX, 'red');
      drawRotationChart(rotationCharts.rotationY, data.eulerY, 'green');
      drawRotationChart(rotationCharts.rotationZ, data.eulerZ, 'blue');

      
    } catch (err) {
      console.error("Error parsing position data:", err);
    }
  }
};

function updateChart(chart, dataPoint) {
  const dataSeries = chart.options.data[0].dataPoints;

  dataSeries.push(dataPoint);

  // Limiting the number of data points to 25
  if (dataSeries.length > 25) {
      dataSeries.shift();
  }
  chart.render();
}
