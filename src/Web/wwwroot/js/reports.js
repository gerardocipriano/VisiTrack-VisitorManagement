import Vue from "vue";
import VisitorChart from "./components/VisitorChart.vue";

new Vue({
  el: "#chartContainer",
  components: { VisitorChart },
  data: {
    visitorData: null,
  },
  methods: {
    fetchVisitorData(startDate, endDate) {
      fetch(
        "/reports/getweeklyvisitors?startDate=" +
          startDate.toISOString() +
          "&endDate=" +
          endDate.toISOString()
      )
        .then((response) => response.json())
        .then((visitorCount) => {
          this.visitorData = {
            labels: ["Lun", "Mar", "Mer", "Gio", "Ven", "Sab", "Dom"],
            datasets: [
              {
                label: "Numero di visitatori",
                backgroundColor: "#f87979",
                data: visitorCount,
              },
            ],
          };
        });
    },
  },
  mounted() {
    var weekPicker = document.getElementById("weekPicker");
    weekPicker.addEventListener(
      "change",
      function () {
        var weekInfo = this.value.split("-");
        var year = weekInfo[0];
        var week = weekInfo[1].substring(1);
        var startDate = new Date(year, 0, 1 + (week - 1) * 7);
        var endDate = new Date(startDate);
        endDate.setDate(endDate.getDate() + 6);
        this.fetchVisitorData(startDate, endDate);
      }.bind(this)
    );
  },
});
