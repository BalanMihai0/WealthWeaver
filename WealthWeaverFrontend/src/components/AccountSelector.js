import React from "react";

/**
 * This components provides a dropdown to select and account from the list of accounts
 * @param {accounts, selectedId} @see mock-data/bankAccounts.json
 * @returns
 */
export default function AccountSelector({ accounts, selectedId = "1" }) {
  return (
    <div className="py-2 px-2 bg-blue-500 rounded-full inline-block shadow-lg">
      <select
        name="account-selection"
        className=" bg-blue-500 text-white tracking-tighter"
        defaultValue={selectedId}
      >
        {accounts.map((account) => (
          <option key={account.id} value={account.id}>
            {account.accountName} - (x{account.last4})
          </option>
        ))}
      </select>
    </div>
  );
}
