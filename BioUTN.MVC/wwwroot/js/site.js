// Lógica PWA y Bandeja Digital
let selectedFrascos = new Set();

function initBandeja() {
  const grid = document.getElementById('bandejaGrid');
  if(!grid) return;
  
  // Attach event listeners to Razor generated buttons
  const btns = grid.querySelectorAll('.frasco-btn');
  btns.forEach(btn => {
      const id = btn.getAttribute('data-id');
      btn.onclick = () => toggleFrasco(btn, id);
  });
}

function toggleFrasco(btnElement, id) {
  if(btnElement.classList.contains('state-fungi') || 
     btnElement.classList.contains('state-bacteria') || 
     btnElement.classList.contains('state-dead')) {
    return;
  }
  
  if(selectedFrascos.has(id)) {
    selectedFrascos.delete(id);
    btnElement.classList.remove('selected');
  } else {
    selectedFrascos.add(id);
    btnElement.classList.add('selected');
  }
  updateBulkActionsVisibility();
}

function updateBulkActionsVisibility() {
  const bar = document.getElementById('bulkActions');
  const countSpan = document.getElementById('selectedCount');
  if(!bar) return;
  
  if(selectedFrascos.size > 0) {
    bar.classList.add('show');
    countSpan.innerText = selectedFrascos.size;
  } else {
    bar.classList.remove('show');
  }
}

function applyBulkAction(type) {
  if(selectedFrascos.size === 0) return;
  
  const confirmMsg = `¿Confirmas marcar ${selectedFrascos.size} frascos como ${type}? (Soft Delete aplicado)`;
  if(confirm(confirmMsg)) {
    selectedFrascos.forEach(id => {
      const btn = document.querySelector(`.frasco-btn[data-id="${id}"]`);
      if(btn) {
          btn.classList.remove('selected');
          if(type === 'Hongos') btn.classList.add('state-fungi');
          if(type === 'Bacterias') btn.classList.add('state-bacteria');
          if(type === 'Baja/Muerte') btn.classList.add('state-dead');
      }
    });
    
    selectedFrascos.clear();
    updateBulkActionsVisibility();
  }
}

function simulateQRScan() {
  setTimeout(() => {
    window.location.href = '/Cultivo/ScannerResult';
  }, 500);
}

// --- RF-03 Session Watchdog ---
let inactivityTimer;
let warningModal;

function resetSessionWatchdog() {
    clearTimeout(inactivityTimer);
    
    // Hide modal if it's open
    if(warningModal) {
        warningModal.hide();
    }
    
    // Set timer for 30 seconds (simulation of 28 minutes before warning)
    inactivityTimer = setTimeout(() => {
        const modalEl = document.getElementById('sessionTimeoutModal');
        if(modalEl) {
            warningModal = new bootstrap.Modal(modalEl);
            warningModal.show();
            
            // Auto logout after 10 seconds of ignoring the warning (simulation of the final 2 minutes)
            setTimeout(() => {
                if(modalEl.classList.contains('show')) {
                    window.location.href = '/Account/Login';
                }
            }, 10000); 
        }
    }, 30000); // 30 seconds for prototype
}

document.addEventListener('DOMContentLoaded', () => {
  initBandeja();
  
  // Initialize watchdog only if we are not on the login page
  if(window.location.pathname.toLowerCase().indexOf('/account/login') === -1) {
      resetSessionWatchdog();
      // Reset timer on any user activity
      document.addEventListener('mousemove', resetSessionWatchdog);
      document.addEventListener('keypress', resetSessionWatchdog);
      document.addEventListener('click', resetSessionWatchdog);
      document.addEventListener('scroll', resetSessionWatchdog);
  }
});
