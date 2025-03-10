import React from "react";
import AccountsPage from "./pages/AccountsPage";
import TransactionsPage from "./pages/TransactionsPage";
import { Routes, Route } from "react-router-dom";
import TransferPage from "./pages/TransferPage";
import LoginPage from "./pages/LoginPage";
import ProfilePage from "./pages/ProfilePage";
import TransferSuccessPage from "./pages/TransferSuccessPage";

/**
 * Main Component of the application.
 * Contains all the routing configurations
 *
 */
export default function App() {
  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
      <Route path="/accounts" element={<AccountsPage />} />
      <Route path="/transactions" element={<TransactionsPage />} />
      <Route path="/transfer" element={<TransferPage />} />
      <Route path="/transfer-success" element={<TransferSuccessPage />} />
      <Route path="/profile" element={<ProfilePage />} />
    </Routes>
  );
}
